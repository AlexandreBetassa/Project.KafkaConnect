using Confluent.Kafka;

namespace Project.Contracts.src
{
    public interface IPublisherService
    {
        public static IProducer<string, string>? Producer { get; set; }
        public Task Send(string topic, string key, string value);
        public IProducer<string, string> GetClientPublisher();
    }
}
