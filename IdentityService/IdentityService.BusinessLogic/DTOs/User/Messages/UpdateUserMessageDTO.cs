using IdentityService.BusinessLogic.DTOs.Base.Messages;

namespace IdentityService.BusinessLogic.DTOs.User.Messages
{
    public class UpdateUserMessageDTO : MessageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public UpdateUserMessageDTO() : base(MessageType.Update)
        {
        }
    }
}
