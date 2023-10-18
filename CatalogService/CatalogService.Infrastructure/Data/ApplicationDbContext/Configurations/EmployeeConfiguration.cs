using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.Data.ApplicationDbContext.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee");

            builder.Property(employee => employee.Name).HasMaxLength(50);

            builder.HasKey(employee => employee.Id);
            builder.Property(employee => employee.Id).ValueGeneratedNever();

            builder.HasOne(employee => employee.Restaurant).WithMany(r => r.Employees);
        }
    }
}
