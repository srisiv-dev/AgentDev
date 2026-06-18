# Production MQ Handler

Register:

```csharp
builder.Services.Configure<MqOptions>(builder.Configuration.GetSection("Mq"));
builder.Services.AddSingleton<IMqHandler,MqHandler>();
```

This is a starter implementation and should be extended with reconnect retry logic,
structured logging enrichment and IBM MQ reason-code handling for your environment.
