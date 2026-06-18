using System.Threading;
using System.Threading.Tasks;

public interface IMqHandler : IAsyncDisposable
{
    Task WriteMessageAsync<T>(T message,CancellationToken cancellationToken=default);
    Task<T?> ReadMessageAsync<T>(CancellationToken cancellationToken=default);
    bool IsHealthy { get; }
}
