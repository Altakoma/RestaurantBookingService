using AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
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

        public async Task<bool> IsWorkingAtRestaurant(int employeeId, int restaurantId,
            CancellationToken cancellationToken)
        {
            bool isWorkAtRestaurant = await _catalogServiceDbContext.Restaurants
                .Where(restaurant => restaurant.Id == restaurantId)
                .AnyAsync(restaurant => 
                restaurant.Employees.Any(employee => employee.Id == employeeId),
                cancellationToken);

            return isWorkAtRestaurant;
        }
    }
}
