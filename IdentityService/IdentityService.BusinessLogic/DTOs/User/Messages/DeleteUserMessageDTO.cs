using IdentityService.BusinessLogic.DTOs.Base.Messages;

namespace IdentityService.BusinessLogic.DTOs.User.Messages
{
    public class DeleteUserMessageDTO : MessageDTO
    {
        public int Id { get; set; }

        public DeleteUserMessageDTO() : base(MessageType.Delete)
        {
        }
    }
}
