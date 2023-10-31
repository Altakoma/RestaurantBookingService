namespace BookingService.Application.DTOs.Booking
{
    public class UpdateBookingDTO
    {
        public int ClientId { get; set; }
        public int TableId { get; set; }
        public DateTime BookingTime { get; set; }
    }
}
