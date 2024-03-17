using IdentityService.BusinessLogic.DTOs.Base.Messages;

namespace IdentityService.BusinessLogic.DTOs.User.Messages
{
    public class InsertUserMessageDTO : MessageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public InsertUserMessageDTO() : base(MessageType.Insert)
        {
        }
    }
}
