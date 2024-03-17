using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CatalogService.Infrastructure.Data.ApplicationDbContext
{
    public class CatalogServiceDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<FoodType> FoodTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public CatalogServiceDbContext(DbContextOptions options) : base(options)
        {
        }

        public CatalogServiceDbContext() : base()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
