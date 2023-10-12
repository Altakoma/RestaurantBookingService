namespace CatalogService.Application.DTOs.Menu
{
    public class UpdateMenuDTO
    {
        public string FoodName { get; set; } = null!;
        public int FoodTypeId { get; set; }
        public int RestaurantId { get; set; }
        public double Cost { get; set; }
    }
}
