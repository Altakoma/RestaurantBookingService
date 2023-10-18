using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.Data.ApplicationDbContext.Configurations
{
    public class FoodTypeConfiguration : IEntityTypeConfiguration<FoodType>
    {
        public void Configure(EntityTypeBuilder<FoodType> builder)
        {
            builder.ToTable("FoodType");

            builder.HasKey(foodType => foodType.Id);

            builder.Property(foodType => foodType.Name).HasMaxLength(100);

            builder.HasMany(foodType => foodType.Menu).WithOne(m => m.FoodType);
        }
    }
}
