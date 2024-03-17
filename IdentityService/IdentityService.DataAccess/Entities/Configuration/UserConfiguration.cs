using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.DataAccess.Entities.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder.ToTable("User");

            builder.Property(user => user.Name).HasMaxLength(50);
            builder.Property(user => user.Login).HasMaxLength(50);
            builder.Property(user => user.Password).HasMaxLength(50);

            builder.Property(user => user.UserRoleId).IsRequired();
            builder.Property(user => user.Login).IsRequired();
            builder.Property(user => user.Name).IsRequired();
            builder.Property(user => user.Password).IsRequired();

            builder.HasOne(user => user.UserRole).WithMany(userRole => userRole.Users)
                .HasForeignKey(user => user.UserRoleId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
