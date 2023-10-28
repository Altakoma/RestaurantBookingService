using MediatR;
using OrderService.Application.Interfaces.Command;

namespace OrderService.Application.MediatR.Order.Commands
{
    public class DeleteOrderCommand : IRequest, ITransactional
    {
        public int Id { get; set; }
    }
}
