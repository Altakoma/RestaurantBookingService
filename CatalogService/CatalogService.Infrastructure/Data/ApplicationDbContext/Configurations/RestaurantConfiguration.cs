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

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name).HasMaxLength(50);
            builder.Property(r => r.City).HasMaxLength(50);
            builder.Property(r => r.Street).HasMaxLength(50);
            builder.Property(r => r.House).HasMaxLength(50);

            builder.HasMany(r => r.Menu).WithMany(m => m.Restaurants);
            builder.HasMany(r => r.Employees).WithMany(e => e.Restaurants);
        }
    }
}
