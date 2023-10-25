using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingService.Infrastructure.Data.ApplicationDbContext.Configurations
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable("Restaurant");

            builder.Property(restaurant => restaurant.Name).HasMaxLength(50);

            builder.HasKey(restaurant => restaurant.Id);
            builder.Property(restaurant => restaurant.Id).ValueGeneratedNever();

            builder.HasMany(restaurant => restaurant.Tables)
                   .WithOne(table => table.Restaurant);
        }
    }
}
