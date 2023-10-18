namespace BookingService.Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = null!;
    }
}
