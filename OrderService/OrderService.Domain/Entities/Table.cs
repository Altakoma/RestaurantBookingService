namespace OrderService.Domain.Entities
{
    public class Table : BaseEntity
    {
        public int RestaurantId { get; set; }

        public ICollection<Order> Orders { get; set; } = null!;
    }
}
