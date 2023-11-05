namespace IdentityService.BusinessLogic.DTOs.Base.Messages
{
    public class MessageDTO
    {
        public MessageType Type { get; set; }

        public MessageDTO(MessageType messageType)
        {
            Type = messageType;
        }
    }

    public enum MessageType
    {
        Delete,
        Update,
        Insert
    }
}
