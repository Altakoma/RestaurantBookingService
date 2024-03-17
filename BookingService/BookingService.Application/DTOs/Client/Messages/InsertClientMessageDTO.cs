using BookingService.Application.DTOs.Base.Messages;

namespace BookingService.Application.DTOs.Client.Messages
{
    public class InsertClientMessageDTO : MessageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
