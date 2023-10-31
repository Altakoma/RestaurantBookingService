namespace BookingService.Domain.Entities
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; } = null!;

        public ICollection<Table> Tables { get; set; } = null!;
    }
}
