using CatalogService.Application.DTOs.Menu;

namespace CatalogService.Application.Services.Interfaces
{
    public interface IMenuService
    {
        Task<ReadMenuDTO> InsertAsync(InsertMenuDTO item);
        Task<ReadMenuDTO> UpdateAsync(int id, UpdateMenuDTO item);
        Task DeleteAsync(int id);
        Task<ReadMenuDTO> GetByIdAsync(int id);
        Task<ICollection<ReadMenuDTO>> GetAllAsync();
    }
}
