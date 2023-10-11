namespace CatalogService.Domain.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string FoodName { get; set; } = null!;
        public int FoodTypeId { get; set; }
        public double Cost { get; set; }

        public FoodType FoodType { get; set; } = null!;
        public ICollection<Restaurant> Restaurants { get; set; } = null!;
    }
}
