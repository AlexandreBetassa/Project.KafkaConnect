using Confluent.Kafka;

namespace Project.Domain.src.Contracts
{
    public interface ISubscribeOptions
    {
        public string Topic { get; set; }
        public string BootstrapServer { get; set; }
        public string GroupId { get; set; }
        public string ClientId { get; set; }
        public bool AutoCommit { get; set; }
        public bool ApiVersionRequest { get; set; }
        public AutoOffsetReset AutoOffsetResetApp { get; set; }
        public int TimeConsume { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
