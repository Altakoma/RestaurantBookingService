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

            builder.HasKey(menu => menu.Id);

            builder.Property(menu => menu.FoodName).HasMaxLength(100);

            builder.HasOne(menu => menu.Restaurant).WithMany(r => r.Menu);
            builder.HasOne(menu => menu.FoodType).WithMany(ft => ft.Menu);
        }
    }
}
