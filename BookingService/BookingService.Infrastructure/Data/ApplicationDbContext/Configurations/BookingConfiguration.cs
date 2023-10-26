using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingService.Infrastructure.Data.ApplicationDbContext.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Booking");

            builder.HasOne(booking => booking.Client)
                   .WithMany(client => client.Bookings);

            builder.HasOne(booking => booking.Table)
                   .WithOne(table => table.Booking);

            builder.HasKey(booking => booking.Id);
        }
    }
}
