namespace BookingService.Application.DTOs.Booking
{
    public class ReadBookingDTO
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; } = null!;
        public int TableId { get; set; }
        public string RestaurantName { get; set; } = null!;
        public DateTime BookingTime { get; set; }
    }
}
