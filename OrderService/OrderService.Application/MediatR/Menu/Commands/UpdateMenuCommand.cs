using MediatR;
using OrderService.Application.DTOs.Menu;

namespace OrderService.Application.MediatR.Menu.Commands
{
    public class UpdateMenuCommand : IRequest<ReadMenuDTO>
    {
        public int Id { get; set; }
        public string FoodName { get; set; } = null!;
    }
}
