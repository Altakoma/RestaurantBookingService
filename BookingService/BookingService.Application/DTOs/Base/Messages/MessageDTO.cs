namespace BookingService.Application.DTOs.Base.Messages
{
    public class MessageDTO
    {
        public MessageType Type { get; set; }
    }

    public enum MessageType
    {
        Delete,
        Update,
        Insert
    }
}
