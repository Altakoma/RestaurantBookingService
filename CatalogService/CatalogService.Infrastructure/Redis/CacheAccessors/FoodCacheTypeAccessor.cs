using CatalogService.Application.Interfaces.Repositories.Base;
using CatalogService.Application.Redis.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Redis.Base;
using Microsoft.Extensions.Caching.Distributed;

namespace CatalogService.Infrastructure.Redis.CacheAccessors
{
    public class FoodCacheTypeAccessor : BaseCacheAccessor<FoodType>, IFoodTypeCacheAccessor
    {
        private const string FoodTypePreposition = "foodtype";
        private const int FoodTypeCacheExpirationTime = 30;

        public FoodCacheTypeAccessor(IRepository<FoodType> repository,
            IDistributedCache distributedCache)
            : base(repository, distributedCache, FoodTypeCacheExpirationTime,
                   FoodTypePreposition)
        {
        }
    }
}
