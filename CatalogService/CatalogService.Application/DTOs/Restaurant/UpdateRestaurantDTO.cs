namespace CatalogService.Application.DTOs.Restaurant
{
    public class UpdateRestaurantDTO
    {
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string House { get; set; } = null!;
    }
}
