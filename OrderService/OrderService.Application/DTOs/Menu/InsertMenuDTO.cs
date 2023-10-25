namespace OrderService.Application.DTOs.Menu
{
    public class InsertMenuDTO
    {
        public int Id { get; set; }
        public string FoodName { get; set; } = null!;
        public double Cost { get; set; }
    }
}
