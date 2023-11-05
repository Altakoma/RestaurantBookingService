using BookingService.Application.DTOs.Base.Messages;

namespace BookingService.Application.DTOs.Client.Messages
{
    public class DeleteUserMessageDTO : MessageDTO
    {
        public int Id { get; set; }
    }
}
