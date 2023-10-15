namespace CatalogService.Application.DTOs.Restaurant
{
    public class ReadRestaurantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string House { get; set; } = null!;
    }
}
