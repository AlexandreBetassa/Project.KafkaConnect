using Confluent.Kafka;
using Project.Contracts.src;

namespace Project.Kafka.src
{
    public class KafkaService : IKafkaService
    {
        private readonly IKafkaOptions _kafkaOptions;
        private IProducer<string, string>? Producer { get; set; }

        public KafkaService(IKafkaOptions kafkaOptions)
        {
            _kafkaOptions = kafkaOptions;
        }

        public IProducer<string, string> GetClientKafka()
        {
            var clientProducerConfig = new ProducerConfig
            {
                BootstrapServers = _kafkaOptions.BootstrapServer,
                ApiVersionRequest = _kafkaOptions.ApiVersionRequest,
                ClientId = _kafkaOptions.ClientId,
                Acks = _kafkaOptions.AcksApp,
                LingerMs = _kafkaOptions.LingerMs,
                EnableDeliveryReports = _kafkaOptions.EnableDeliveryReports,
                SecurityProtocol = _kafkaOptions.Protocol,
                EnableSaslOauthbearerUnsecureJwt = true
            };
            var producer = new ProducerBuilder<string, string>(clientProducerConfig).Build();
            return producer;
        }

        public async Task Send(string topic, string key, string value)
        {
            try
            {
                Producer = GetClientKafka();
                await Producer.ProduceAsync(topic, new Message<string, string> { Key = key, Value = value });
                Producer.Flush();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Producer!.Dispose();
            }
        }
    }
}
