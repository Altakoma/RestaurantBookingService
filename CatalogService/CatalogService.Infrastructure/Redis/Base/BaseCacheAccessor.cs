using CatalogService.Application.Interfaces.Repositories.Base;
using CatalogService.Application.Redis.Interfaces.Base;
using CatalogService.Domain.Exceptions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CatalogService.Infrastructure.Redis.Base
{
    public abstract class BaseCacheAccessor<T> : ICacheAccessor
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IRepository<T> _repository;
        private readonly string _keyPreposition;
        private readonly int _expirationTimeInMinutes;

        public BaseCacheAccessor(IRepository<T> repository,
            IDistributedCache distributedCache,
            int expirationTimeInMinutes,
            string keyPreposition)
        {
            _distributedCache = distributedCache;
            _keyPreposition = keyPreposition;
            _repository = repository;
            _expirationTimeInMinutes = expirationTimeInMinutes;
        }

        public async Task<U> GetByResourceIdAsync<U>(string resourceId,
            CancellationToken cancellationToken)
        {
            string key = _keyPreposition + resourceId;

            string? cachedResource = await _distributedCache.GetStringAsync(
                key, cancellationToken);

            if (string.IsNullOrEmpty(cachedResource))
            {
                U itemDTO = await TryGetAsync<U>(resourceId, cancellationToken);

                string resource = JsonSerializer.Serialize(itemDTO);

                await SetAsync(resourceId, resource, cancellationToken);

                return itemDTO;
            }

            U? deserializedItemDTO = JsonSerializer.Deserialize<U>(cachedResource);

            if (deserializedItemDTO is null)
            {
                throw new JsonException(ExceptionMessages.BadDeserialization);
            }

            return deserializedItemDTO;
        }

        private async Task<U> TryGetAsync<U>(string resourceId,
            CancellationToken cancellationToken)
        {
            bool isParsed = int.TryParse(resourceId, out int id);

            if (!isParsed)
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.ValueIsNotNumber, resourceId));
            }

            U? itemDTO = await _repository
                .GetByIdAsync<U>(id, cancellationToken);

            if (itemDTO is null)
            {
                throw new NotFoundException(typeof(T).Name,
                    id.ToString(), typeof(T));
            }

            return itemDTO;
        }

        private async Task SetAsync(string resourceId, string resource,
            CancellationToken cancellationToken)
        {
            string key = _keyPreposition + resourceId;

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_expirationTimeInMinutes),
            };

            await _distributedCache.SetStringAsync(key, resource,
                options, cancellationToken);
        }
    }
}
