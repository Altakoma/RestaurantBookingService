using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Data.ApplicationSqlDbContext.Configurations
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("Table");

            builder.HasKey(table => table.Id);
            builder.Property(table => table.Id).ValueGeneratedNever();

            builder.HasMany(table => table.Orders)
                   .WithOne(order => order.Table);
        }
    }
}
