using IdentityService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdentityServiceDataAccess.DatabaseContext
{
    public partial class IdentityDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
