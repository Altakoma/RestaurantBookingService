namespace OrderService.Application.DTOs.Order
{
    public class UpdateOrderDTO
    {
        public int ClientId { get; set; }
        public int TableId { get; set; }
        public int MenuId { get; set; }
    }
}
