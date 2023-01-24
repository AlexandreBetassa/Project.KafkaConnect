using Confluent.Kafka;
using Project.Models.src.Contracts;
using System.Text.Json.Serialization;

namespace Project.Models.src.Entities
{
    public class KafkaOptions : IKafkaOptions
    {
        [JsonPropertyName("Topic")]
        public string Topic { get; set; }
        [JsonPropertyName("BootstrapServer")]
        public string BootstrapServer { get; set; }
        [JsonPropertyName("GroupId")]
        public string GroupId { get; set; }
        [JsonPropertyName("ClientId")]
        public string ClientId { get; set; }
        [JsonPropertyName("AutoCommit")]
        public bool AutoCommit { get; set; }
        [JsonPropertyName("ApiVersionRequest")]
        public bool ApiVersionRequest { get; set; }
        public AutoOffsetReset AutoOffsetResetApp { get; set; } = AutoOffsetReset.Earliest;
        [JsonPropertyName("TimeConsume")]
        public int TimeConsume { get; set; }
    }
}
