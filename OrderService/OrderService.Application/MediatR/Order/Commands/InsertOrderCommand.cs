using MediatR;
using OrderService.Application.DTOs.Order;

namespace OrderService.Application.MediatR.Order.Commands
{
    public class InsertOrderCommand : IRequest<ReadOrderDTO>
    {
        public int ClientId { get; set; }
        public int TableId { get; set; }
        public int MenuId { get; set; }
    }
}
