using CatalogService.Application.DTOs.Restaurant;

namespace CatalogService.Application.DTOs.Employee
{
    public class ReadEmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string RestaurantName { get; set; } = null!;
    }
}
