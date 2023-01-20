using Microsoft.EntityFrameworkCore;
using Project.Contracts.src;
using Project.Infra.src.Context;

namespace Project.Infra.src.Database
{
    public class Data<T> : IDatabase<T> where T : class
    {
        private readonly AppDbContext _ctx;

        public Data(AppDbContext context)
        {
            _ctx = context;
        }
        public Task<T> Create(T entity)
        {
            var result = _ctx.Set<T>().Add(entity);
            _ctx.SaveChanges();
            return Task.FromResult(result.Entity);
        }

        public Task<List<T>> GetAll()
        {
            return _ctx.Set<T>().ToListAsync();
        }

        public Task<T> GetOne(int id)
        {
            var result = _ctx.Set<T>().Find(id);
            return Task.FromResult(result)!;
        }

        public Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
