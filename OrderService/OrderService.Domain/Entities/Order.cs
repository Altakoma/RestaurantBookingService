namespace OrderService.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int ClientId { get; set; }
        public int MenuId { get; set; }
        public int BookingId { get; set; }

        public Menu Menu { get; set; } = null!;
        public Client Client { get; set; } = null!;
    }
}
