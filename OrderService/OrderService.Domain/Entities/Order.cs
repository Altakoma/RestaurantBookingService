namespace OrderService.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int ClientId { get; set; }
        public int TableId { get; set; }
        public int MenuId { get; set; }

        public Menu Menu { get; set; } = null!;
        public Table Table { get; set; } = null!;
        public Client Client { get; set; } = null!;
    }
}
