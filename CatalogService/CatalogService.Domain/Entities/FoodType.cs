namespace CatalogService.Domain.Entities
{
    public class FoodType : BaseEntity
    {
        public string Name { get; set; } = null!;

        public ICollection<Menu> Menu { get; set; } = null!;
    }
}
