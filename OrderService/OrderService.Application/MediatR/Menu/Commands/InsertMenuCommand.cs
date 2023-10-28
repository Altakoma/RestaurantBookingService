using MediatR;
using OrderService.Application.DTOs.Menu;
using OrderService.Application.Interfaces.Command;

namespace OrderService.Application.MediatR.Menu.Commands
{
    public class InsertMenuCommand : IRequest<ReadMenuDTO>, ITransactional
    {
        public int Id { get; set; }
        public string FoodName { get; set; } = null!;
    }
}
