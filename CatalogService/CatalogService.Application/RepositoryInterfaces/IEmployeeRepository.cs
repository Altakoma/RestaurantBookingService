using CatalogService.Application.RepositoryInterfaces.Base;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.RepositoryInterfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<ICollection<Employee>> GetAllByRestaurantIdAsync(int id);
    }
}
