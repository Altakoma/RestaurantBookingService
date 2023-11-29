using OrderService.Application.DTOs.Client;
using OrderService.Application.DTOs.Menu;
using OrderService.Domain.Entities;

namespace OrderService.Application.DTOs.Order
{
    public class ReadOrderDTO : BaseEntity
    {
        public int BookingId { get; set; }
        public ReadMenuDTO ReadMenuDTO { get; set; } = null!;
        public ReadClientDTO ReadClientDTO { get; set; } = null!;
    }
}
