using IdentityService.BusinessLogic.DTOs.TokenDTOs;
using IdentityService.BusinessLogic.DTOs.UserDTOs;
using IdentityService.DataAccess.Entities;

namespace IdentityService.BusinessLogic.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<TokensDTO?> VerifyAndGenerateToken(TokensDTO tokenDTO);
        Task<RefreshToken?> GetByUserId(int id);
        Task InsertAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
        Task DeleteAsync(int id);
        Task SaveToken(RefreshToken token);
    }
}
