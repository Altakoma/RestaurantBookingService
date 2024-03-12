using MediatR;
using OrderService.Application.Interfaces.Command;

namespace OrderService.Application.MediatR.Order.Commands
{
    public class DeleteOrderCommand : ITransactional, IRequest
    {
        public int Id { get; set; }
    }
}
