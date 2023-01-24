using Project.Models.src.Contracts;

namespace Project.Services.src
{
    public class Services<T> : IServices<T> where T : class
    {
        private readonly IRepository<T> _repository;
        public Services(IRepository<T> repository)
        {
            _repository = repository;
        }
        public Task<T> Create(T entity)
        {
            return _repository.Create(entity);
        }

        public Task<List<T>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<T> GetOne(int id)
        {
            return _repository.GetOne(id);
        }

        public Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
