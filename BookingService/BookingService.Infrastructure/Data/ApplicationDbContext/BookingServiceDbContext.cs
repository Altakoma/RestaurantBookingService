using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BookingService.Infrastructure.Data.ApplicationDbContext
{
    public class BookingServiceDbContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

        public BookingServiceDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public BookingServiceDbContext() : base()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
