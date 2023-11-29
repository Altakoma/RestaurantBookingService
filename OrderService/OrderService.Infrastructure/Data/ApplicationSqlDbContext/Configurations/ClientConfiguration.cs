using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Data.ApplicationSqlDbContext.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");

            builder.Property(client => client.Name).HasMaxLength(50);

            builder.HasKey(client => client.Id);
            builder.Property(client => client.Id).ValueGeneratedNever();

            builder.HasMany(client => client.Orders)
                   .WithOne(order => order.Client);
        }
    }
}
