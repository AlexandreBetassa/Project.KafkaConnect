using Microsoft.EntityFrameworkCore;
using Project.Domain.src.Entities;

namespace Project.Infra.src.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Vote> Votes { get; set; }
    }
}
