using Confluent.Kafka;
using Project.Contracts.src;

namespace Project.Infra.src.PubSub.Publisher
{
    public class KafkaService : IPublisherService
    {
        private readonly IPublisherOptions _publisherOptions;
        private IProducer<string, string>? Producer { get; set; }

        public KafkaService(IPublisherOptions publisherOptions)
        {
            _publisherOptions = publisherOptions;
        }

        public IProducer<string, string> GetClientPublisher()
        {
            var clientProducerConfig = new ProducerConfig
            {
                BootstrapServers = _publisherOptions.BootstrapServer,
                ApiVersionRequest = _publisherOptions.ApiVersionRequest,
                ClientId = _publisherOptions.ClientId,
                Acks = _publisherOptions.AcksApp,
                LingerMs = _publisherOptions.LingerMs,
                EnableDeliveryReports = _publisherOptions.EnableDeliveryReports,
                SecurityProtocol = _publisherOptions.Protocol,
            };
            var producer = new ProducerBuilder<string, string>(clientProducerConfig).Build();
            return producer;
        }

        public async Task Send(string topic, string key, string value)
        {
            try
            {
                Producer = GetClientPublisher();
                await Producer.ProduceAsync(topic, new Message<string, string> { Key = key, Value = value });
                Producer.Flush();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
