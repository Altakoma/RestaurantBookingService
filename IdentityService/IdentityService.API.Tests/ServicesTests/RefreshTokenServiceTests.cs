using FluentAssertions;
using IdentityService.API.Tests.Fakers;
using IdentityService.API.Tests.Mocks.CacheAccessors;
using IdentityService.API.Tests.Mocks.Generators;
using IdentityService.API.Tests.Mocks.Repositories;
using IdentityService.API.Tests.Mocks.Services;
using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.Services;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Exceptions;
using Moq;

namespace IdentityService.API.Tests.ServicesTests
{
    public class RefreshTokenServiceTests
    {
        private readonly RefreshTokenCacheAccessorMock _refreshTokenAccessorMock;
        private readonly JwtTokenGeneratorMock _tokenGeneratorMock;
        private readonly CookieServiceMock _cookieServiceMock;
        private readonly UserRepositoryMock _userRepositoryMock;

        private readonly RefreshTokenService _refreshTokenService;

        public RefreshTokenServiceTests()
        {
            _refreshTokenAccessorMock = new();
            _tokenGeneratorMock = new();
            _cookieServiceMock = new();
            _userRepositoryMock = new();

            _refreshTokenService = new RefreshTokenService(_refreshTokenAccessorMock.Object,
                _tokenGeneratorMock.Object, _cookieServiceMock.Object, _userRepositoryMock.Object);
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId, RefreshTokenDataFaker.StandartMaximumRefreshTokenLength)]
        public async Task VerifyAndGenerateTokenAsync_WhenTokenIsValid_ReturnsAccessToken(
            int maxId, int refreshTokenLength)
        {
            //Arrange
            int userId = UserDataFaker.GetRandomNumber(maxId);
            string refreshToken = RefreshTokenDataFaker.GetRandomRefreshToken(refreshTokenLength);

            User user = UserDataFaker.GetFakedUser(userId);

            AccessTokenDTO accessTokenDTO = UserDataFaker.GetFakedAccessTokenDTO();

            _cookieServiceMock.MockGetCookieValue(CookieService.UserIdCookieName,
                user.Id.ToString())
            .MockGetCookieValue(CookieService.RefreshTokenCookieName, refreshToken);

            _refreshTokenAccessorMock.MockGetByUserIdAsync(userId.ToString(), refreshToken)
                                     .MockSetAsync(userId.ToString(), refreshToken);

            _userRepositoryMock.MockGetByIdAsync(userId, user);

            _tokenGeneratorMock.MockGenerateTokens(user.Name, user.UserRole.Name,
                user.Id.ToString(), refreshToken, accessTokenDTO);

            _cookieServiceMock.MockSetCookieValue(CookieService.RefreshTokenCookieName,
                refreshToken);

            //Act
            var result = await _refreshTokenService
                .VerifyAndGenerateTokenAsync(It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(accessTokenDTO);

            _cookieServiceMock.Verify();
            _refreshTokenAccessorMock.Verify();
            _userRepositoryMock.Verify();
            _tokenGeneratorMock.Verify();
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId, RefreshTokenDataFaker.StandartMaximumRefreshTokenLength)]
        public async Task VerifyAndGenerateTokenAsync_WhenTokenIsNotValid_ThrowsNotFoundException(
            int maxId, int refreshTokenLength)
        {
            //Arrange
            int userId = UserDataFaker.GetRandomNumber(maxId);

            string cookieRefreshTooken = RefreshTokenDataFaker.GetRandomRefreshToken(refreshTokenLength);
            string refreshToken = RefreshTokenDataFaker.GetRandomRefreshToken(refreshTokenLength);

            User user = UserDataFaker.GetFakedUser(userId);

            _cookieServiceMock.MockGetCookieValue(CookieService.UserIdCookieName,
                user.Id.ToString())
            .MockGetCookieValue(CookieService.RefreshTokenCookieName, cookieRefreshTooken);

            _refreshTokenAccessorMock.MockGetByUserIdAsync(userId.ToString(), refreshToken);

            //Act
            var result = _refreshTokenService.VerifyAndGenerateTokenAsync(It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _cookieServiceMock.Verify();
            _refreshTokenAccessorMock.Verify();
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId, RefreshTokenDataFaker.StandartMaximumRefreshTokenLength)]
        public async Task SetAsync_SuccessfullyExecuted(int maxId, int refreshTokenLength)
        {
            //Arrange
            int userId = UserDataFaker.GetRandomNumber(maxId);
            string refreshToken = RefreshTokenDataFaker.GetRandomRefreshToken(refreshTokenLength);

            _refreshTokenAccessorMock.MockSetAsync(userId.ToString(), refreshToken);

            //Act
            await _refreshTokenService.SetAsync(userId.ToString(), refreshToken,
                RefreshTokenService.ExpirationTime, It.IsAny<CancellationToken>());

            //Assert
            _refreshTokenAccessorMock.Verify();
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public async Task DeleteByIdAsync_SuccessfullyExecuted(int maxId)
        {
            //Arrange
            int userId = UserDataFaker.GetRandomNumber(maxId);

            _refreshTokenAccessorMock.MockDeleteByIdAsync(userId.ToString());

            //Act
            await _refreshTokenService.DeleteByIdAsync(userId.ToString(),
                It.IsAny<CancellationToken>());

            //Assert
            _refreshTokenAccessorMock.Verify();
        }
    }
}
