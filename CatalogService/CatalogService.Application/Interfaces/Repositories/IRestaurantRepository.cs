using CatalogService.Application.Interfaces.Repositories.Base;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces.Repositories
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        Task<bool> IsWorkingAtRestaurant(int employeeId, int restaurantId,
            CancellationToken cancellationToken);
    }
}
