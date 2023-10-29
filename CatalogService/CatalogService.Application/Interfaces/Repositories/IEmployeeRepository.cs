using CatalogService.Application.Interfaces.Repositories.Base;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<ICollection<U>> GetAllByRestaurantIdAsync<U>(int id, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
        Task<bool> WorksAtRestaurantAsync(int employeeId, int restaurantId, CancellationToken cancellationToken);
    }
}
