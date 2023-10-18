using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.DataAccess.Entities.Configuration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshToken");

            builder.Property(refreshToken => refreshToken.isRevoked).IsRequired();
            builder.Property(refreshToken => refreshToken.AddedDate).IsRequired();
            builder.Property(refreshToken => refreshToken.ExpirationDate).IsRequired();
            builder.Property(refreshToken => refreshToken.Token).IsRequired();

            builder.HasOne(refreshToken => refreshToken.User).WithOne(user => user.RefreshToken)
                .HasForeignKey<RefreshToken>(refreshToken => refreshToken.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(r => r.Id);
        }
    }
}
