namespace CatalogService.Application.DTOs.Menu
{
    public class ReadMenuDTO
    {
        public int Id { get; set; }
        public string FoodName { get; set; } = null!;
        public int FoodTypeId { get; set; }
        public double Cost { get; set; }
    }
}
