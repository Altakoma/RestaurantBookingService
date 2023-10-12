using CatalogService.Application.DTOs.Employee;

namespace CatalogService.Application.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<ReadEmployeeDTO> InsertAsync(InsertEmployeeDTO item);
        Task<ReadEmployeeDTO> UpdateAsync(int id, UpdateEmployeeDTO item);
        Task DeleteAsync(int id);
        Task<ReadEmployeeDTO> GetByIdAsync(int id);
        Task<ICollection<ReadEmployeeDTO>> GetAllAsync();
        Task<ICollection<ReadEmployeeDTO>> GetAllByRestaurantIdAsync(int id);
    }
}
