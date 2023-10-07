using IdentityService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.DataAccess.Entities.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.ToTable("User");

            builder.Property(u => u.Name).HasMaxLength(50);
            builder.Property(u => u.Login).HasMaxLength(50);
            builder.Property(u => u.Password).HasMaxLength(50);

            builder.Property(u => u.UserRoleId).IsRequired();
            builder.Property(u => u.Login).IsRequired();
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.Password).IsRequired();

            builder.HasOne(u => u.UserRole).WithMany(ur => ur.Users)
                .HasForeignKey(u => u.UserRoleId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
