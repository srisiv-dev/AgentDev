using IBM.WMQ;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

public sealed class MqHandler:IMqHandler
{
    private readonly ILogger<MqHandler> _logger;
    private readonly MqOptions _options;
    private readonly SemaphoreSlim _connectionLock=new(1,1);
    private readonly SemaphoreSlim _writeLock=new(1,1);
    private readonly SemaphoreSlim _readLock=new(1,1);
    private readonly Channel<string> _channel;
    private MQQueueManager? _qm;
    private MQQueue? _queue;
    private readonly CancellationTokenSource _cts=new();
    private readonly Task _worker;
    public bool IsHealthy=>_qm!=null&&_queue!=null&&!_channel.Reader.Completion.IsCompleted;

    public MqHandler(IOptions<MqOptions> options,ILogger<MqHandler> logger)
    {
        _logger=logger;
        _options=options.Value;
        _channel=Channel.CreateBounded<string>(new BoundedChannelOptions(_options.ChannelCapacity)
        {
            SingleReader=true,
            SingleWriter=false,
            FullMode=BoundedChannelFullMode.Wait
        });
        _worker=Task.Run(ProcessAsync);
    }

    private async Task EnsureConnectedAsync()
    {
        if(_qm!=null&&_queue!=null)return;
        await _connectionLock.WaitAsync();
        try
        {
            if(_qm!=null&&_queue!=null)return;
            var props=new Hashtable{
                {MQC.HOST_NAME_PROPERTY,_options.Host},
                {MQC.PORT_PROPERTY,_options.Port},
                {MQC.CHANNEL_PROPERTY,_options.Channel},
                {MQC.USER_ID_PROPERTY,_options.User},
                {MQC.PASSWORD_PROPERTY,_options.Password},
                {MQC.TRANSPORT_PROPERTY,MQC.TRANSPORT_MQSERIES_CLIENT}
            };
            _qm=new MQQueueManager(_options.QueueManager,props);
            _queue=_qm.AccessQueue(_options.QueueName,MQC.MQOO_INPUT_SHARED|MQC.MQOO_OUTPUT);
        }
        finally{_connectionLock.Release();}
    }

    public async Task WriteMessageAsync<T>(T message,CancellationToken cancellationToken=default)
    {
        var json=JsonSerializer.Serialize(message);
        await _channel.Writer.WriteAsync(json,cancellationToken);
    }

    private async Task ProcessAsync()
    {
        await foreach(var json in _channel.Reader.ReadAllAsync(_cts.Token))
        {
            await EnsureConnectedAsync();
            await _writeLock.WaitAsync();
            try
            {
                var msg=new MQMessage();
                msg.WriteString(json);
                _queue!.Put(msg,new MQPutMessageOptions());
            }
            finally{_writeLock.Release();}
        }
    }

    public async Task<T?> ReadMessageAsync<T>(CancellationToken cancellationToken=default)
    {
        await EnsureConnectedAsync();
        await _readLock.WaitAsync(cancellationToken);
        try
        {
            var msg=new MQMessage();
            var gmo=new MQGetMessageOptions{
                WaitInterval=_options.WaitIntervalMs,
                Options=MQC.MQGMO_WAIT|MQC.MQGMO_FAIL_IF_QUIESCING
            };
            _queue!.Get(msg,gmo);
            var text=msg.ReadString(msg.DataLength);
            return JsonSerializer.Deserialize<T>(text);
        }
        finally{_readLock.Release();}
    }

    public async ValueTask DisposeAsync()
    {
        _channel.Writer.TryComplete();
        _cts.Cancel();
        try{await _worker;}catch{}
        _queue?.Close();
        _qm?.Disconnect();
        _connectionLock.Dispose();
        _writeLock.Dispose();
        _readLock.Dispose();
        _cts.Dispose();
    }
}
