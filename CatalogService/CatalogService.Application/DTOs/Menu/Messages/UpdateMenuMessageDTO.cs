using CatalogService.Application.DTOs.Base.Messages;

namespace CatalogService.Application.DTOs.Menu.Messages
{
    public class UpdateMenuMessageDTO : MessageDTO
    {
        public int Id { get; set; }
        public string FoodName { get; set; } = null!;
        public double Cost { get; set; }

        public UpdateMenuMessageDTO() : base(MessageType.Update)
        {
        }
    }
}
