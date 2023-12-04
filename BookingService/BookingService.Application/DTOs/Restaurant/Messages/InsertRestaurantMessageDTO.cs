using BookingService.Application.DTOs.Base.Messages;

namespace BookingService.Application.DTOs.Restaurant.Messages
{
    public class InsertRestaurantMessageDTO : MessageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
