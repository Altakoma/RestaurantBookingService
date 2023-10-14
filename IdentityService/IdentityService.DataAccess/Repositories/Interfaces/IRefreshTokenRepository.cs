using IdentityService.DataAccess.DTOs.RefreshToken;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Interfaces.Base;

namespace IdentityService.DataAccess.Repositories.Interfaces
{
    public interface IRefreshTokenRepository : IWriteRepository<RefreshToken>
    {
        Task<RefreshToken?> GetByUserIdAsync(int id, CancellationToken cancellationToken);
        Task<CreationRefreshTokenDTO?> GetCreationRefreshTokenDTOAsync(string token, CancellationToken cancellationToken);
    }
}
