namespace CatalogService.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Restaurant> Restaurants { get; set; } = null!;
    }
}
