using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Data.ApplicationSqlDbContext.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menu");

            builder.Property(menu => menu.FoodName).HasMaxLength(100);

            builder.HasKey(menu => menu.Id);
            builder.Property(menu => menu.Id).ValueGeneratedNever();

            builder.HasMany(menu => menu.Orders)
                   .WithOne(order => order.Menu);
        }
    }
}
