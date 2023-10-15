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

            builder.HasKey(ft => ft.Id);

            builder.Property(ft => ft.Name).HasMaxLength(100);

            builder.HasMany(ft => ft.Menu).WithOne(m => m.FoodType);
        }
    }
}
