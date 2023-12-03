using CatalogService.Domain.Entities;

namespace CatalogService.Application.DTOs.FoodType
{
    public class ReadFoodTypeDTO : BaseEntity
    {
        public string Name { get; set; } = null!;
    }
}
