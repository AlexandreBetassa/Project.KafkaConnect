using Confluent.Kafka;
using Project.Domain.src.Contracts;

namespace Project.Domain.src.Entities
{
    public class KafkaOptions : IPublisherOptions
    {
        public string Topic { get; set; }
        public string BootstrapServer { get; set; }
        public string ClientId { get; set; }
        public bool ApiVersionRequest { get; set; }
        public int LingerMs { get; set; }
        public bool EnableDeliveryReports { get; set; }
        public Acks AcksApp { get; set; }
        public SecurityProtocol Protocol { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
