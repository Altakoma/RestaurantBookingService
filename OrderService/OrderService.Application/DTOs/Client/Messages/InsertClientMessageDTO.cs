using OrderService.Application.DTOs.Base.Messages;

namespace OrderService.Application.DTOs.Client.Messages
{
    public class InsertClientMessageDTO : MessageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
