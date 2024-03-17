using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Data.ApplicationSqlDbContext.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(order => order.Id);

            builder.HasOne(order => order.Menu)
                   .WithMany(menu => menu.Orders);

            builder.HasOne(order => order.Client)
                   .WithMany(client => client.Orders);
        }
    }
}
