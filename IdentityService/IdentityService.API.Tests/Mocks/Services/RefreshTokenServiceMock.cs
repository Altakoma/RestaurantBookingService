using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.Services;
using IdentityService.BusinessLogic.Services.Interfaces;
using Moq;

namespace IdentityService.API.Tests.Mocks.Services
{
    public class RefreshTokenServiceMock : Mock<IRefreshTokenService>
    {
        public RefreshTokenServiceMock MockVerifyAndGenerateTokenAsync(
            AccessTokenDTO accessToken)
        {
            Setup(refreshTokenService =>
            refreshTokenService.VerifyAndGenerateTokenAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(accessToken)
            .Verifiable();

            return this;
        }

        public RefreshTokenServiceMock MockSetAsync(string userId,
            string refreshToken)
        {
            Setup(refreshTokenService => refreshTokenService.SetAsync(
                userId, refreshToken, RefreshTokenService.ExpirationTime,
                It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }
        public RefreshTokenServiceMock MockDeleteAsync(string userId)
        {
            Setup(refreshTokenService =>
                refreshTokenService.DeleteByIdAsync(userId, It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }
    }
}
