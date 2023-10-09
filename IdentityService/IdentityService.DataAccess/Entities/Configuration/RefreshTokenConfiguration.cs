using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.DataAccess.Entities.Configuration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshToken");

            builder.Property(r => r.IsUsed).IsRequired();
            builder.Property(r => r.isRevoked).IsRequired();
            builder.Property(r => r.AddedDate).IsRequired();
            builder.Property(r => r.ExpirationDate).IsRequired();
            builder.Property(r => r.Token).IsRequired();

            builder.HasOne(r => r.User).WithOne(u => u.RefreshToken)
                .HasForeignKey<RefreshToken>(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(r => r.UserId);
        }
    }
}
