namespace BookingService.Domain.Entities
{
    public class Table
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int SeatsCount { get; set; }

        public Restaurant Restaurant { get; set; } = null!;
        public Booking Booking { get; set; } = null!;
    }
}
