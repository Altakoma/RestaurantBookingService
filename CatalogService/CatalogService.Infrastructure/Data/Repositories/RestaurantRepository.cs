using AutoMapper;
using CatalogService.Application.DTOs.Restaurant;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using CatalogService.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Data.Repositories
{
    public class RestaurantRepository : WriteRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(CatalogServiceDbContext dbContext,
            IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            Restaurant? restaurant = await _dbContext.Restaurants
                .FirstOrDefaultAsync(restaurant => restaurant.Id == id);

            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Employee), id.ToString(),
                    typeof(Employee));
            }

            Delete(restaurant);
        }

        public async Task<ICollection<ReadRestaurantDTO>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var readRestaurantDTOs = await  _mapper.ProjectTo<ReadRestaurantDTO>(
                _dbContext.Restaurants.Select(restaurant => restaurant))
                .ToListAsync();

            return readRestaurantDTOs;
        }

        public async Task<ReadRestaurantDTO?> GetByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            var readRestaurantDTO = await _mapper.ProjectTo<ReadRestaurantDTO>(
                _dbContext.Restaurants.Where(restaurant => restaurant.Id == id))
                .SingleOrDefaultAsync();

            return readRestaurantDTO;
        }
    }
}
