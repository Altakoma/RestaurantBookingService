using CatalogService.Application.DTOs.FoodType;

namespace CatalogService.Application.Services.Interfaces
{
    public interface IFoodTypeService
    {
        Task<ReadFoodTypeDTO> InsertAsync(FoodTypeDTO item, CancellationToken cancellationToken);
        Task<ReadFoodTypeDTO> UpdateAsync(int id, FoodTypeDTO item, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<ReadFoodTypeDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<ICollection<ReadFoodTypeDTO>> GetAllAsync(CancellationToken cancellationToken);
    }
}
