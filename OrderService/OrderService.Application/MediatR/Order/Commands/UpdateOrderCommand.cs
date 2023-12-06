using MediatR;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Command;

namespace OrderService.Application.MediatR.Order.Commands
{
    public class UpdateOrderCommand : ITransactional, IRequest<ReadOrderDTO>
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int MenuId { get; set; }
    }
}
