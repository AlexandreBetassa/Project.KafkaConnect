using Microsoft.EntityFrameworkCore;
using Project.Models.src.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
