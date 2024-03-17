using CatalogService.Application.DTOs.Base.Messages;

namespace CatalogService.Application.DTOs.Menu.Messages
{
    public class DeleteMenuMessageDTO : MessageDTO
    {
        public int Id { get; set; }

        public DeleteMenuMessageDTO() : base(MessageType.Delete)
        {
        }
    }
}
