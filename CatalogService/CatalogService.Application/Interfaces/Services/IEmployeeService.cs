using CatalogService.Application.DTOs.Employee;
using CatalogService.Domain.Interfaces.Services;

namespace CatalogService.Application.Interfaces.Services
{
    public interface IEmployeeService : IBaseEmployeeService
    {
        Task<T> InsertAsync<T>(InsertEmployeeDTO employee, CancellationToken cancellationToken);
    }
}
