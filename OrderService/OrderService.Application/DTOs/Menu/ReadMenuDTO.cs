namespace OrderService.Application.DTOs.Menu
{
    public class ReadMenuDTO
    {
        public int Id { get; set; }
        public string FoodName { get; set; } = null!;
        public double Cost { get; set; }
    }
}
