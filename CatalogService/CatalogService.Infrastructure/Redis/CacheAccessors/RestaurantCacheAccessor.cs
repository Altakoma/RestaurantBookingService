using CatalogService.Application.Interfaces.Repositories.Base;
using CatalogService.Application.Redis.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Redis.Base;
using Microsoft.Extensions.Caching.Distributed;

namespace CatalogService.Infrastructure.Redis.CacheAccessors
{
    public class RestaurantCacheAccessor : BaseCacheAccessor<Restaurant>, IRestaurantCacheAccessor
    {
        private const string RestaurantPreposition = "restaurant";
        private const int RestaurantCacheExpiratonTime = 30;

        public RestaurantCacheAccessor(IRepository<Restaurant> repository,
            IDistributedCache distributedCache)
            : base(repository, distributedCache, RestaurantCacheExpiratonTime,
                   RestaurantPreposition)
        {
        }
    }
}
