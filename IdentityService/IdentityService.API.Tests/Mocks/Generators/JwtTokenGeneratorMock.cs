using FluentAssertions;
using IdentityService.API.Tests.Mocks.Producers;
using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.BusinessLogic.TokenGenerators;
using Moq;

namespace IdentityService.API.Tests.Mocks.Generators
{
    public class JwtTokenGeneratorMock : Mock<ITokenGenerator>
    {
        public JwtTokenGeneratorMock MockGenerateTokens(string name, string roleName,
            string id, string refreshToken, AccessTokenDTO accessTokenDTO)
        {
            Setup(tokenGenerator => tokenGenerator.GenerateTokens(name, roleName, id))
                .Returns((accessTokenDTO, refreshToken))
            .Verifiable();

            return this;
        }
    }
}
