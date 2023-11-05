using CatalogService.Application.DTOs.Base.Messages;

namespace CatalogService.Application.DTOs.Restaurant.Messages
{
    public class DeleteRestaurantMessageDTO : MessageDTO
    {
        public int Id { get; set; }

        public DeleteRestaurantMessageDTO() : base(MessageType.Delete)
        {
        }
    }
}
