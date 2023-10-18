using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.DataAccess.Entities.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(userRole => userRole.Id);

            builder.ToTable("UserRole");

            builder.Property(userRole => userRole.Name).HasMaxLength(20);

            builder.Property(userRole => userRole.Name).IsRequired();

            builder.HasMany(userRole => userRole.Users).WithOne(u => u.UserRole);
        }
    }
}
