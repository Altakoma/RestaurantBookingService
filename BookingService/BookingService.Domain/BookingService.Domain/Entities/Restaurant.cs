namespace BookingService.Domain.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Table> Tables { get; set; } = null!;
    }
}
