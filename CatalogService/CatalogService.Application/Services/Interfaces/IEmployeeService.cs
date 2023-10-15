using CatalogService.Application.DTOs.Employee;

namespace CatalogService.Application.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<ReadEmployeeDTO> InsertAsync(InsertEmployeeDTO item, CancellationToken cancellationToken);
        Task<ReadEmployeeDTO> UpdateAsync(int id, UpdateEmployeeDTO item, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<ReadEmployeeDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<ICollection<ReadEmployeeDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<ICollection<ReadEmployeeDTO>> GetAllByRestaurantIdAsync(int id, CancellationToken cancellationToken);
    }
}
