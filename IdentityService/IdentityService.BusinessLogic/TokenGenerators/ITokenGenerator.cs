using IdentityService.BusinessLogic.DTOs.TokenDTOs;
using IdentityService.DataAccess.Entities;

namespace IdentityService.BusinessLogic.TokenGenerators
{
    public interface ITokenGenerator
    {
        (TokenDTO, RefreshToken) GenerateToken(string name, string roleName, int accountId);
    }
}
