using BookingService.Domain.Entities;

namespace BookingService.Application.DTOs.Table
{
    public class ReadTableDTO : BaseEntity
    {
        public int RestaurantId { get; set; }
        public int SeatsCount { get; set; }
        public string RestaurantName { get; set; } = null!;
    }
}
