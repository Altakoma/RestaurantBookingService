using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.DataAccess.Entities;

namespace IdentityService.BusinessLogic.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<TokenDTO> VerifyAndGenerateTokenAsync();
        Task<RefreshToken> GetByUserIdAsync(int id);
        Task InsertAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
        Task DeleteAsync(int id);
        Task SaveTokenAsync(RefreshToken token);
        void SetRefreshTokenCookie(string refreshToken);
    }
}
