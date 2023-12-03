using CatalogService.Application.DTOs.Restaurant;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.DTOs.Employee
{
    public class ReadEmployeeDTO : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string RestaurantName { get; set; } = null!;
    }
}
