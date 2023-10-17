using AutoMapper;
using CatalogService.Application.DTOs.Restaurant;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Application.Services.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;

namespace CatalogService.Application.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public RestaurantService(IRestaurantRepository restaurantRepository,
            IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
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

        public async Task<ICollection<ReadRestaurantDTO>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadRestaurantDTO> readRestaurantDTOs =
                await _restaurantRepository.GetAllAsync<ReadRestaurantDTO>(cancellationToken);

            return readRestaurantDTOs;
        }

        public async Task<ReadRestaurantDTO> GetByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            ReadRestaurantDTO? readRestaurantDTO = await _restaurantRepository
                .GetByIdAsync<ReadRestaurantDTO>(id, cancellationToken);

            if (readRestaurantDTO is null)
            {
                throw new NotFoundException(nameof(Restaurant),
                    id.ToString(), typeof(Restaurant));
            }

            return readRestaurantDTO;
        }

        public async Task<ReadRestaurantDTO> InsertAsync(InsertRestaurantDTO item,
            CancellationToken cancellationToken)
        {
            var restaurant = _mapper.Map<Restaurant>(item);

            restaurant = await _restaurantRepository
                               .InsertAsync(restaurant, cancellationToken);

            bool isInserted = await _restaurantRepository
                                    .SaveChangesToDbAsync(cancellationToken);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertAsync),
                    restaurant.Id.ToString(), typeof(Restaurant));
            }

            var readRestaurantDTO = _mapper.Map<ReadRestaurantDTO>(restaurant);

            return readRestaurantDTO;
        }

        public async Task<ReadRestaurantDTO> UpdateAsync(int id,
            UpdateRestaurantDTO item, CancellationToken cancellationToken)
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

            var readRestaurantDTO = _mapper.Map<ReadRestaurantDTO>(restaurant);

            return readRestaurantDTO;
        }
    }
}
