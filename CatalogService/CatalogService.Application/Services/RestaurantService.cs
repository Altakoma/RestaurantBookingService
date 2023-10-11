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

        public async Task DeleteAsync(int id)
        {
            Restaurant? restaurant = await _restaurantRepository.GetByIdAsync(id);

            if (restaurant is null)
            {
                throw new NotFoundException(id.ToString(), typeof(Restaurant));
            }

            bool isDeleted = await _restaurantRepository.DeleteAsync(restaurant);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteAsync), id.ToString(),
                    typeof(Restaurant));
            }
        }

        public async Task<ICollection<ReadRestaurantDTO>> GetAllAsync()
        {
            ICollection<Restaurant> restaurants = await _restaurantRepository.GetAllAsync();

            var readRestaurantDTOs = _mapper.Map<ICollection<ReadRestaurantDTO>>(restaurants);

            return readRestaurantDTOs;
        }

        public async Task<ReadRestaurantDTO> GetByIdAsync(int id)
        {
            Restaurant? restaurant = await _restaurantRepository.GetByIdAsync(id);

            if (restaurant is null)
            {
                throw new NotFoundException(id.ToString(), typeof(Restaurant));
            }

            var readRestaurantDTO = _mapper.Map<ReadRestaurantDTO>(restaurant);

            return readRestaurantDTO;
        }

        public async Task<ReadRestaurantDTO> InsertAsync(InsertRestaurantDTO item)
        {
            var restaurant = _mapper.Map<Restaurant>(item);

            (restaurant, bool isInserted) = await _restaurantRepository
                .InsertAsync(restaurant);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertAsync),
                    restaurant.Id.ToString(), typeof(Restaurant));
            }

            var readRestaurantDTO = _mapper.Map<ReadRestaurantDTO>(restaurant);

            return readRestaurantDTO;
        }

        public async Task<ReadRestaurantDTO> UpdateAsync(int id, UpdateRestaurantDTO item)
        {
            var restaurant = _mapper.Map<Restaurant>(item);

            bool isUpdated = await _restaurantRepository.UpdateAsync(restaurant);

            if (!isUpdated)
            {
                throw new DbOperationException(nameof(UpdateAsync), id.ToString(),
                    typeof(Restaurant));
            }

            restaurant = await _restaurantRepository.GetByIdAsync(id);

            var readRestaurantDTO = _mapper.Map<ReadRestaurantDTO>(restaurant);

            return readRestaurantDTO;
        }
    }
}
