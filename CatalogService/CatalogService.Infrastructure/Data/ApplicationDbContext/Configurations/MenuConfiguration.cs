using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.Data.ApplicationDbContext.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menu");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.FoodName).HasMaxLength(100);

            builder.HasOne(m => m.Restaurant).WithMany(r => r.Menu);
            builder.HasOne(m => m.FoodType).WithMany(ft => ft.Menu);
        }
    }
}
