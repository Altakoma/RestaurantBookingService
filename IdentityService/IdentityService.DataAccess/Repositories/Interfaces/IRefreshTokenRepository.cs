using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Interfaces.Base;

namespace IdentityService.DataAccess.Repositories.Interfaces
{
    public interface IRefreshTokenRepository : ICreateUpdateDeleteRepository<RefreshToken>
    {
        Task<RefreshToken?> GetByUserIdAsync(int id);
        Task<User?> GetUserByRefreshToken(string refreshToken);
    }
}
