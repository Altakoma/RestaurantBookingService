using CatalogService.Application.Interfaces.Repositories.Base;
using CatalogService.Application.Redis.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Redis.Base;
using Microsoft.Extensions.Caching.Distributed;

namespace CatalogService.Infrastructure.Redis.CacheAccessors
{
    public class MenuCacheAccessor : BaseCacheAccessor<Menu>, IMenuCacheAccessor
    {
        private const string MenuCachePreposition = "menu";
        private const int MenuCacheExpirationTime = 30;

        public MenuCacheAccessor(IRepository<Menu> repository,
            IDistributedCache distributedCache)
            : base(repository, distributedCache, MenuCacheExpirationTime,
                   MenuCachePreposition)
        {
        }
    }
}
