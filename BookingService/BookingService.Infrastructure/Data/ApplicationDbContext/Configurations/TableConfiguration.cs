using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingService.Infrastructure.Data.ApplicationDbContext.Configurations
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("Table");

            builder.HasKey(table => table.Id);

            builder.HasOne(table => table.Booking)
                   .WithOne(booking => booking.Table);

            builder.HasOne(table => table.Restaurant)
                   .WithMany(restaurant => restaurant.Tables);
        }
    }
}
