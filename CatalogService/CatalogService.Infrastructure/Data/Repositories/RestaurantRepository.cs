using AutoMapper;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using CatalogService.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Data.Repositories
{
    public class RestaurantRepository : BaseRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(CatalogServiceDbContext catalogServiceDbContext,
            IMapper mapper) : base(catalogServiceDbContext, mapper)
        {
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            Restaurant? restaurant = await _catalogServiceDbContext.Restaurants
                .FirstOrDefaultAsync(restaurant => restaurant.Id == id,
                cancellationToken);

            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Employee), id.ToString(),
                    typeof(Employee));
            }

            Delete(restaurant);
        }

        public async Task<U?> GetByIdAsync<U>(int id,
            CancellationToken cancellationToken)
        {
            U? readRestaurantDTO = await _mapper.ProjectTo<U>(
                _catalogServiceDbContext.Restaurants.Where(restaurant => restaurant.Id == id))
                .SingleOrDefaultAsync(cancellationToken);

            return readRestaurantDTO;
        }
    }
}
