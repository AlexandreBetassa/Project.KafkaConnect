using Project.Models.src.Contracts;

namespace Project.Repository.src
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDatabase<T> _db;

        public Repository(IDatabase<T> db)
        {
            _db = db;
        }

        public async Task<T> Create(T entity) => await _db.Create(entity);

        public async Task<List<T>> GetAll() => await _db.GetAll();

        public async Task<T> GetOne(int id) => await _db.GetOne(id);

        public Task<T> Update(T entity) => _db.Update(entity);
    }
}
