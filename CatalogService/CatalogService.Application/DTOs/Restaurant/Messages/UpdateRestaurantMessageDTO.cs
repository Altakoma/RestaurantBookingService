using CatalogService.Application.DTOs.Base.Messages;

namespace CatalogService.Application.DTOs.Restaurant.Messages
{
    public class UpdateRestaurantMessageDTO : MessageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public UpdateRestaurantMessageDTO() : base(MessageType.Update)
        {
        }
    }
}
