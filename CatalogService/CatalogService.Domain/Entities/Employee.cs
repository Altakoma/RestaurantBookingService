namespace CatalogService.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = null!;

        public Restaurant Restaurant { get; set; } = null!;
    }
}
