using IdentityService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdentityService.DataAccess.DatabaseContext
{
    public class IdentityDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        public IdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        public IdentityDbContext() : base()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
