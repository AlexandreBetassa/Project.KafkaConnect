using Confluent.Kafka;

namespace Project.Contracts.src
{
    public interface IKafkaOptions
    {
        public string Topic { get; set; }
        public string BootstrapServer { get; set; }
        public string ClientId { get; set; }
        public bool ApiVersionRequest { get; set; }
        public int LingerMs { get; set; }
        public bool EnableDeliveryReports { get; set; }
        public Acks AcksApp { get; }
        public SecurityProtocol Protocol { get; }
    }
}
