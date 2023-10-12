namespace CatalogService.Application.DTOs.Employee
{
    public class UpdateEmployeeDTO
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = null!;
    }
}
