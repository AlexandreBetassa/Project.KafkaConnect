namespace Project.Domain.src.Contracts
{
    public interface ISubscribeService
    {
        Task ListenerServer(CancellationToken cancellation);
    }
}
