using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using System.Reflection;

namespace OrderService.Infrastructure.Data.ApplicationSQLDbContext
{
    public class OrderServiceSqlDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Order> Orders { get; set; }

        public OrderServiceSqlDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
