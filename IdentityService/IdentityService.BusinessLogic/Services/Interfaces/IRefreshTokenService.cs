using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.DataAccess.Entities;

namespace IdentityService.BusinessLogic.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        string RefreshTokenCookie { get; set; }
        Task<TokenDTO> VerifyAndGenerateTokenAsync(CancellationToken cancellationToken);
        Task<RefreshToken?> GetByUserIdAsync(int id, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task SaveTokenAsync(RefreshToken token, CancellationToken cancellationToken);
    }
}
