using BookingService.Application.DTOs.Base.Messages;

namespace BookingService.Application.DTOs.Client.Messages
{
    public class UpdateClientMessageDTO : MessageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
