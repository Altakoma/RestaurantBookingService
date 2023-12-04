using IdentityService.DataAccess.CacheAccess.Interfaces;
using IdentityService.DataAccess.Exceptions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace IdentityService.DataAccess.CacheAccess
{
    public class RefreshTokenCacheAccessor : IRefreshTokenCacheAccessor
    {
        private const string KeyPreposition = "refresh-";
        private readonly IDistributedCache _distributedCache;

        public RefreshTokenCacheAccessor(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<string> GetByUserIdAsync(string userId,
            CancellationToken cancellationToken)
        {
            string key = KeyPreposition + userId;

            string? cachedRefresh = await _distributedCache.GetStringAsync(
                key, cancellationToken);

            if (string.IsNullOrEmpty(cachedRefresh))
            {
                throw new NotFoundException(nameof(cachedRefresh),
                    typeof(string));
            }

            return cachedRefresh;
        }

        public async Task SetAsync(string userId, string refreshToken,
            DistributedCacheEntryOptions options,
            CancellationToken cancellationToken)
        {
            string key = KeyPreposition + userId;

            await _distributedCache.SetStringAsync(key, refreshToken, options,
                cancellationToken);
        }

        public async Task DeleteByIdAsync(string userId,
            CancellationToken cancellationToken)
        {
            string key = KeyPreposition + userId;

            await _distributedCache.RemoveAsync(key, cancellationToken);
        }
    }
}
