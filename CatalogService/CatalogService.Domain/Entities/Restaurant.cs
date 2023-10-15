namespace CatalogService.Domain.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string House { get; set; } = null!;

        public ICollection<Employee> Employees { get; set; } = null!;
        public ICollection<Menu> Menu { get; set; } = null!;
    }
}
