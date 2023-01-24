using Confluent.Kafka;
using Project.Contracts.src;

namespace Project.Models.src.Entities
{
    public class KafkaOptions : IPublisherOptions
    {
        public string Topic { get; set; }
        public string BootstrapServer { get; set; }
        public string ClientId { get; set; }
        public bool ApiVersionRequest { get; set; }
        public int LingerMs { get; set; }
        public bool EnableDeliveryReports { get; set; }
        public Acks AcksApp { get; } = Acks.All;
        public SecurityProtocol Protocol { get; } = SecurityProtocol.Plaintext;

    }
}
