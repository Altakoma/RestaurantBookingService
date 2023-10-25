using CatalogService.Application.DTOs.FoodType;

namespace CatalogService.Application.DTOs.Menu
{
    public class ReadMenuDTO
    {
        public int Id { get; set; }
        public string FoodName { get; set; } = null!;
        public string RestaurantName { get; set; } = null!;
        public double Cost { get; set; }
        public ReadFoodTypeDTO FoodTypeDTO { get; set; } = null!;
    }
}
