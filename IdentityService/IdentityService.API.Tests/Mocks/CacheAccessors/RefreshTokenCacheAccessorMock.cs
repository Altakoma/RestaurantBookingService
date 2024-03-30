using IdentityService.DataAccess.CacheAccess.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Moq;

namespace IdentityService.API.Tests.Mocks.CacheAccessors
{
    public class RefreshTokenCacheAccessorMock : Mock<IRefreshTokenCacheAccessor>
    {
        public RefreshTokenCacheAccessorMock MockGetByUserIdAsync(string userId,
            string refreshToken)
        {
            Setup(refreshTokenAccessor =>
                refreshTokenAccessor.GetByUserIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(refreshToken)
            .Verifiable();

            return this;
        }

        public RefreshTokenCacheAccessorMock MockSetAsync(string userId,
            string refreshToken)
        {
            Setup(refreshTokenAccessor =>
                refreshTokenAccessor.SetAsync(userId, refreshToken,
                It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }

        public RefreshTokenCacheAccessorMock MockDeleteByIdAsync(string userId)
        {
            Setup(refreshTokenAccessor =>
                refreshTokenAccessor.DeleteByIdAsync(userId, It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }
    }
}
