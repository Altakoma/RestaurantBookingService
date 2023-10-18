using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.Data.ApplicationDbContext.Configurations
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable("Restaurant");

            builder.HasKey(restaurant => restaurant.Id);

            builder.Property(restaurant => restaurant.Name).HasMaxLength(50);
            builder.Property(restaurant => restaurant.City).HasMaxLength(50);
            builder.Property(restaurant => restaurant.Street).HasMaxLength(50);
            builder.Property(restaurant => restaurant.House).HasMaxLength(50);

            builder.HasMany(restaurant => restaurant.Menu).WithOne(m => m.Restaurant);
            builder.HasMany(restaurant => restaurant.Employees).WithOne(e => e.Restaurant);
        }
    }
}
