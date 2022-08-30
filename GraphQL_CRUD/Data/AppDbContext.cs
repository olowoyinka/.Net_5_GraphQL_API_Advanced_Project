using GraphQL_CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_CRUD.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Cake> Cakes { get; set; }
    }
}
