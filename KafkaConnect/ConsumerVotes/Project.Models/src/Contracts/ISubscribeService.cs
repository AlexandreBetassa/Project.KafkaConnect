namespace Project.Models.src.Contracts
{
    public interface ISubscribeService
    {
        Task ListenerServer(CancellationToken cancellation);
    }
}
