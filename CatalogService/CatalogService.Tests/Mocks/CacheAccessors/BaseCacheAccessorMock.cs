using CatalogService.Application.Redis.Interfaces.Base;
using Moq;

namespace CatalogService.Tests.Mocks.CacheAccessors
{
    public class BaseCacheAccessorMock<TService> : Mock<TService> where TService : class, ICacheAccessor
    {
        public BaseCacheAccessorMock<TService> MockGetByResourceIdAsync<T>(string id,
            T entity)
        {
            Setup(cacheAccessor => cacheAccessor.GetByResourceIdAsync<T>(
                id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity)
            .Verifiable();

            return this;
        }

        public BaseCacheAccessorMock<TService> MockDeleteResourceByIdAsync(string id)
        {
            Setup(cacheAccessor => cacheAccessor.DeleteResourceByIdAsync(
                id, It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }
    }
}
