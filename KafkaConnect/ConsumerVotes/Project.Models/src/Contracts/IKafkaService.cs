namespace Project.Models.src.Contracts
{
    public interface IKafkaService
    {
        Task ListenerKafka(CancellationToken cancellation);
    }
}
