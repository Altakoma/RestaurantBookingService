using IdentityService.DataAccess.DatabaseContext;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DataAccess.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IdentityDbContext _identityDbContext;

        public RefreshTokenRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<bool> DeleteAsync(RefreshToken item)
        {
            _identityDbContext.Remove(item);
            return await _identityDbContext.SaveChangesToDbAsync();
        }

        public async Task<RefreshToken?> GetByUserIdAsync(int id)
        {
            var refreshToken = await _identityDbContext
                .RefreshTokens.AsNoTracking()
                .FirstOrDefaultAsync(r => r.UserId == id);

            return refreshToken;
        }

        public async Task<(RefreshToken, bool)> InsertAsync(RefreshToken item)
        {
            await _identityDbContext.AddAsync(item);
            bool isInserted = await _identityDbContext.SaveChangesToDbAsync();

            return (item, isInserted);
        }

        public async Task<bool> UpdateAsync(RefreshToken item)
        {
            _identityDbContext.Update(item);
            return await _identityDbContext.SaveChangesToDbAsync();
        }
    }
}
