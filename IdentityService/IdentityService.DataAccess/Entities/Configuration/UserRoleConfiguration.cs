using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.DataAccess.Entities.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(ur => ur.Id);

            builder.ToTable("UserRole");

            builder.Property(ur => ur.Name).HasMaxLength(20);

            builder.Property(ur => ur.Name).IsRequired();

            builder.HasMany(ur => ur.Users).WithOne(u => u.UserRole);
        }
    }
}
