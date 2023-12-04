using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.DataAccess.Entities;

namespace IdentityService.BusinessLogic.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<AccessTokenDTO> VerifyAndGenerateTokenAsync(CancellationToken cancellationToken);
        Task SetAsync(string userId, string refreshToken, int time,
            CancellationToken cancellationToken);
        Task DeleteByIdAsync(string userId, CancellationToken cancellationToken);
    }
}
