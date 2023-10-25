namespace OrderService.Domain.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; } = null!;

        public ICollection<Order> Orders { get; set; } = null!;
    }
}
