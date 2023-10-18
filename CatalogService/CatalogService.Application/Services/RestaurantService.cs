using AutoMapper;
using CatalogService.Application.DTOs.Restaurant;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Application.Services.Base;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Domain.Interfaces.Services;

namespace CatalogService.Application.Services
{
    public class RestaurantService : BaseService<Restaurant>, IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository,
            IMapper mapper) : base(restaurantRepository, mapper)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            ReadRestaurantDTO? restaurantDTO = await _restaurantRepository
                .GetByIdAsync<ReadRestaurantDTO>(id, cancellationToken);

            if (restaurantDTO is null)
            {
                throw new NotFoundException(nameof(Restaurant),
                    id.ToString(), typeof(Restaurant));
            }

            await _restaurantRepository.DeleteAsync(id, cancellationToken);

            bool isDeleted = await _restaurantRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteAsync), id.ToString(),
                    typeof(Restaurant));
            }
        }

        public async Task<T> GetByIdAsync<T>(int id, CancellationToken cancellationToken)
        {
            T? readRestaurantDTO = await _restaurantRepository
                .GetByIdAsync<T>(id, cancellationToken);

            if (readRestaurantDTO is null)
            {
                throw new NotFoundException(nameof(Restaurant),
                    id.ToString(), typeof(Restaurant));
            }

            return readRestaurantDTO;
        }

        public async Task<T> UpdateAsync<U, T>(int id, U item,
            CancellationToken cancellationToken)
        {
            var restaurant = _mapper.Map<Restaurant>(item);
            restaurant.Id = id;

            _restaurantRepository.Update(restaurant);

            bool isUpdated = await _restaurantRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isUpdated)
            {
                throw new DbOperationException(nameof(UpdateAsync), id.ToString(),
                    typeof(Restaurant));
            }

            var readRestaurantDTO = _mapper.Map<T>(restaurant);

            return readRestaurantDTO;
        }
    }
}
