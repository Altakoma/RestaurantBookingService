using CatalogService.Application.DTOs.FoodType;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.DTOs.Menu
{
    public class ReadMenuDTO : BaseEntity
    {
        public string FoodName { get; set; } = null!;
        public string RestaurantName { get; set; } = null!;
        public double Cost { get; set; }
        public ReadFoodTypeDTO FoodTypeDTO { get; set; } = null!;
    }
}
