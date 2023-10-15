using CatalogService.Application.DTOs.FoodType;

namespace CatalogService.Application.Services.Interfaces
{
    public interface IFoodTypeService
    {
        Task<ReadFoodTypeDTO> InsertAsync(FoodTypeDTO item);
        Task<ReadFoodTypeDTO> UpdateAsync(int id, FoodTypeDTO item);
        Task DeleteAsync(int id);
        Task<ReadFoodTypeDTO> GetByIdAsync(int id);
        Task<ICollection<ReadFoodTypeDTO>> GetAllAsync();
    }
}
