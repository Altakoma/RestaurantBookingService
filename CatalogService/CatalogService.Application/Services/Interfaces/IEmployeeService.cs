using CatalogService.Application.DTOs.Employee;

namespace CatalogService.Application.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> InsertAsync(EmployeeDTO item);
        Task<EmployeeDTO> UpdateAsync(int id, UpdateEmployeeDTO item);
        Task DeleteAsync(int id);
        Task<EmployeeDTO> GetByIdAsync(int id);
        Task<ICollection<EmployeeDTO>> GetAllAsync();
    }
}
