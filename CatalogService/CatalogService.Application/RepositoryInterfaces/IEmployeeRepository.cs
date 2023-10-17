using CatalogService.Application.DTOs.Employee;
using CatalogService.Application.RepositoryInterfaces.Base;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.RepositoryInterfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<U?> GetByIdAsync<U>(int id, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<ICollection<U>> GetAllByRestaurantIdAsync<U>(int id, CancellationToken cancellationToken);
    }
}
