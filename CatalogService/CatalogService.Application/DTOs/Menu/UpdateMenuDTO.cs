namespace CatalogService.Application.DTOs.Menu
{
    public class UpdateMenuDTO : MenuDTO
    {
        public string FoodName { get; set; } = null!;
        public int FoodTypeId { get; set; }
        public double Cost { get; set; }
    }
}
