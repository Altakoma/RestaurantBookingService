namespace BookingService.Application.DTOs.Booking
{
    public class InsertBookingDTO
    {
        public int ClientId { get; set; }
        public int TableId { get; set; }
        public DateTime BookingTime { get; set; }
    }
}
