using CatalogService.Application.DTOs.Menu;

namespace CatalogService.Application.Services.Interfaces
{
    public interface IMenuService
    {
        Task<ReadMenuDTO> InsertAsync(InsertMenuDTO item, CancellationToken cancellationToken);
        Task<ReadMenuDTO> UpdateAsync(int id, UpdateMenuDTO item, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<ReadMenuDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<ICollection<ReadMenuDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<ICollection<ReadMenuDTO>> GetAllByRestaurantIdAsync(int id, CancellationToken cancellationToken);
    }
}
