using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Interfaces.Base;
using Microsoft.Extensions.Caching.Distributed;

namespace IdentityService.DataAccess.CacheAccess.Interfaces
{
    public interface IRefreshTokenCacheAccessor
    {
        Task<string> GetByUserIdAsync(string id, CancellationToken cancellationToken);
        Task SetAsync(string userId, string refreshToken, DistributedCacheEntryOptions options,
            CancellationToken cancellationToken);
    }
}
