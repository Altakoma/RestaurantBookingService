using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.DataAccess.Entities;

namespace IdentityService.BusinessLogic.TokenGenerators
{
    public interface ITokenGenerator
    {
        (TokenDTO, RefreshToken) GenerateToken(string name, string roleName, int accountId);
    }
}
