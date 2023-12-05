using BookingService.Application.DTOs.Table;
using BookingService.Domain.Entities;

namespace BookingService.Application.DTOs.Restaurant
{
    public class ReadRestaurantDTO : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<ReadTableForRestaurantDTO> ReadTableDTOs { get; set; } = null!;
    }
}
