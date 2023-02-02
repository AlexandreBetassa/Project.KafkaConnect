namespace Project.Domain.src.Contracts
{
    public interface IServices<T> where T : class
    {
        Task Create(T entity);
    }
}
