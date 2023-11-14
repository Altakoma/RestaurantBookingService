using MediatR;
using OrderService.Application.Interfaces.Command;

namespace OrderService.Application.MediatR.Order.Commands
{
    public class DeleteOrderCommand : Transactional, IRequest
    {
        public int Id { get; set; }
        public bool IsRequestedBySystem { get; set; } = false;
    }
}
