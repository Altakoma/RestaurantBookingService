using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Interfaces.Base;

namespace IdentityService.DataAccess.Repositories.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken?> GetByUserIdAsync(int id, CancellationToken cancellationToken);
        Task<U?> GetCreationRefreshTokenDTOAsync<U>(string token, CancellationToken cancellationToken);
    }
}
