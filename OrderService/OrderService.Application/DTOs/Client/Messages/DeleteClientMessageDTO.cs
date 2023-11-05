using OrderService.Application.DTOs.Base.Messages;

namespace OrderService.Application.DTOs.Client.Messages
{
    public class DeleteClientMessageDTO : MessageDTO
    {
        public int Id { get; set; }
    }
}
