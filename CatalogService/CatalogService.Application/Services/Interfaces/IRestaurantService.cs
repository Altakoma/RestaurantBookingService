using CatalogService.Application.DTOs.Restaurant;

namespace CatalogService.Application.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<ReadRestaurantDTO> InsertAsync(InsertRestaurantDTO item, CancellationToken cancellationToken);
        Task<ReadRestaurantDTO> UpdateAsync(int id, UpdateRestaurantDTO item, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<ReadRestaurantDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<ICollection<ReadRestaurantDTO>> GetAllAsync(CancellationToken cancellationToken);
    }
}
