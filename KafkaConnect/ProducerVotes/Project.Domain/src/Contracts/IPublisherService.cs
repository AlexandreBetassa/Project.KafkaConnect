using Confluent.Kafka;

namespace Project.Domain.src.Contracts
{
    public interface IPublisherService
    {
        public static IProducer<string, string>? Producer { get; set; }
        public Task Send(string topic, string key, string value);
        public IProducer<string, string> GetClientPublisher();
    }
}
