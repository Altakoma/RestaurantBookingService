using CatalogService.Application.DTOs.Restaurant;

namespace CatalogService.Application.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<ReadRestaurantDTO> InsertAsync(InsertRestaurantDTO item);
        Task<ReadRestaurantDTO> UpdateAsync(int id, UpdateRestaurantDTO item);
        Task DeleteAsync(int id);
        Task<ReadRestaurantDTO> GetByIdAsync(int id);
        Task<ICollection<ReadRestaurantDTO>> GetAllAsync();
    }
}
