namespace CatalogService.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string Name { get; set; } = null!;

        public Restaurant Restaurant { get; set; } = null!;
    }
}
