namespace BookingService.Domain.Entities
{
    public class Booking
    {
        public int ClientId { get; set; }
        public int TableId { get; set; }
        public DateTime BookingTime { get; set; }

        public Client Client { get; set; } = null!;
        public Table Table { get; set; } = null!;
    }
}
