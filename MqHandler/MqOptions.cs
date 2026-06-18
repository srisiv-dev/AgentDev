public sealed class MqOptions
{
    public string QueueManager {get;set;}="";
    public string QueueName {get;set;}="";
    public string Host {get;set;}="";
    public int Port {get;set;}
    public string Channel {get;set;}="";
    public string User {get;set;}="";
    public string Password {get;set;}="";
    public int WaitIntervalMs {get;set;}=5000;
    public int ChannelCapacity {get;set;}=5000;
    public int RetryCount {get;set;}=4;
}
