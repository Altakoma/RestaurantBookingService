using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.DataAccess.Entities;

namespace IdentityService.BusinessLogic.TokenGenerators
{
    public interface ITokenGenerator
    {
        (AccessTokenDTO, string) GenerateTokens(string name, string roleName, string userId);
    }
}
