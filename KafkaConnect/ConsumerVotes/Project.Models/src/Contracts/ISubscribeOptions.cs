using Confluent.Kafka;

namespace Project.Models.src.Contracts
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
    }
}
