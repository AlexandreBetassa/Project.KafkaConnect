using Confluent.Kafka;

namespace Project.Contracts.src
{
    public interface IKafkaService
    {
        Task ListenerKafka(CancellationToken cancellation);
    }
}
