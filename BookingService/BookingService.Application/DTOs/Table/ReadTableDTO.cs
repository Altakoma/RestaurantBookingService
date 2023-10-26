namespace BookingService.Application.DTOs.Table
{
    public class ReadTableDTO
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int SeatsCount { get; set; }
        public string RestaurantName { get; set; } = null!;
    }
}
