using CatalogService.Application.Interfaces.Repositories.Base;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<ICollection<U>> GetAllByRestaurantIdAsync<U>(int id, CancellationToken cancellationToken);
        Task<bool> IsExistingAsync(int id, CancellationToken cancellationToken);
        Task<bool> IsWorkingAtRestaurantAsync(int employeeId, int restaurantId, CancellationToken cancellationToken);
    }
}
