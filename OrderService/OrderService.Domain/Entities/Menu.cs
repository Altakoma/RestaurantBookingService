namespace OrderService.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public string FoodName { get; set; } = null!;
        public double Cost { get; set; }

        public ICollection<Order> Orders { get; set; } = null!;
    }
}
