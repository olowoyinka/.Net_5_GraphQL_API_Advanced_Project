using GraphQL_UserBoarding.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_UserBoarding.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
    }
}