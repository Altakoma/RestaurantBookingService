using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingService.Infrastructure.Data.ApplicationDbContext.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");

            builder.Property(client => client.Name).HasMaxLength(50);

            builder.HasKey(client => client.Id);
            builder.Property(client => client.Id).ValueGeneratedNever();

            builder.HasMany(client => client.Bookings)
                   .WithOne(booking => booking.Client);
        }
    }
}
