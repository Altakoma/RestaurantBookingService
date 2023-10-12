namespace CatalogService.Application.DTOs.Employee
{
    public class InsertEmployeeDTO
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string Name { get; set; } = null!;
    }
}
