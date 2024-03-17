using OrderService.Application.DTOs.Base.Messages;

namespace OrderService.Application.DTOs.Menu.Messages
{
    public class UpdateMenuMessageDTO : MessageDTO
    {
        public int Id { get; set; }
        public string FoodName { get; set; } = null!;
        public double Cost { get; set; }
    }
}
