namespace BookingService.Domain.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = null!;
    }
}
