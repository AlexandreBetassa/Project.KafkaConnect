namespace Project.Contracts.src
{
    public interface IServices<T> where T : class
    {
        Task Create(T entity);
    }
}
