namespace CatalogService.Application.DTOs.Menu
{
    public class InsertMenuDTO : MenuDTO
    {
        public string FoodName { get; set; } = null!;
        public int FoodTypeId { get; set; }
        public double Cost { get; set; }
    }
}
