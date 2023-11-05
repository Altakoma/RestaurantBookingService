using BookingService.Application.DTOs.Base.Messages;

namespace BookingService.Application.DTOs.Restaurant.Messages
{
    public class DeleteRestaurantMessageDTO : MessageDTO
    {
        public int Id { get; set; }
    }
}
