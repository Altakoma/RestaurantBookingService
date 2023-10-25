namespace CatalogService.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public string FoodName { get; set; } = null!;
        public int FoodTypeId { get; set; }
        public int RestaurantId { get; set; }
        public double Cost { get; set; }

        public FoodType FoodType { get; set; } = null!;
        public Restaurant Restaurant { get; set; } = null!;
        public ICollection<Employee> Employees { get; set; } = null!;
    }
}
