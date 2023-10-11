namespace CatalogService.Domain.Entities
{
    public class FoodType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Menu> Menu { get; set; } = null!;
    }
}
