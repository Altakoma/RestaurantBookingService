using AutoMapper;
using CatalogService.Application.DTOs.Restaurant.Messages;
using CatalogService.Application.Interfaces.Kafka.Producers;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Redis.Interfaces;
using CatalogService.Application.Services.Base;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces.Services;

namespace CatalogService.Application.Services
{
    public class RestaurantService : BaseService<Restaurant>, IBaseRestaurantService
    {
        private readonly IRestaurantMessageProducer _restaurantMessageProducer;
        private readonly IRestaurantCacheAccessor _restaurantCacheAccessor;

        public RestaurantService(IRestaurantRepository restaurantRepository,
            IRestaurantMessageProducer restaurantMessageProducer,
            IRestaurantCacheAccessor restaurantCacheAccessor,
            IMapper mapper)
            : base(restaurantRepository, mapper)
        {
            _restaurantMessageProducer = restaurantMessageProducer;
            _restaurantCacheAccessor = restaurantCacheAccessor;
        }

        public override async Task<T> GetByIdAsync<T>(int id,
            CancellationToken cancellationToken)
        {
            T itemDTO = await _restaurantCacheAccessor
                .GetByResourceIdAsync<T>(id.ToString(), cancellationToken);

            return itemDTO;
        }

        public override async Task<int> DeleteAsync(int id,
            CancellationToken cancellationToken)
        {
            await _restaurantCacheAccessor.DeleteResourceByIdAsync(id.ToString(), cancellationToken);

            id = await base.DeleteAsync(id, cancellationToken);

            var message = new DeleteRestaurantMessageDTO { Id = id };

            await _restaurantMessageProducer.ProduceMessageAsync(message, cancellationToken);

            return id;
        }

        public override async Task<T> InsertAsync<U, T>(U item,
            CancellationToken cancellationToken)
        {
            T readRestaurantDTO = await base.InsertAsync<U, T>(item, cancellationToken);

            var message = _mapper.Map<InsertRestaurantMessageDTO>(readRestaurantDTO);

            await _restaurantMessageProducer.ProduceMessageAsync(message, cancellationToken);

            return readRestaurantDTO;
        }

        public override async Task<T> UpdateAsync<U, T>(int id, U item,
            CancellationToken cancellationToken)
        {
            await _restaurantCacheAccessor.DeleteResourceByIdAsync(id.ToString(), cancellationToken);

            T readRestaurantDTO = await base.UpdateAsync<U, T>(id, item, cancellationToken);

            var message = _mapper.Map<UpdateRestaurantMessageDTO>(readRestaurantDTO);

            await _restaurantMessageProducer.ProduceMessageAsync(message, cancellationToken);

            return readRestaurantDTO;
        }
    }
}
