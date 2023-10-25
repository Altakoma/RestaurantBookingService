using OrderService.Application.DTOs.Client;
using OrderService.Application.DTOs.Menu;
using OrderService.Application.DTOs.Table;

namespace OrderService.Application.DTOs.Order
{
    public class ReadOrderDTO
    {
        public int Id { get; set; }
        public ReadMenuDTO ReadMenuDTO { get; set; } = null!;
        public ReadClientDTO ReadClientDTO { get; set; } = null!;
        public ReadTableDTO ReadTableDTO { get; set; } = null!;
    }
}
