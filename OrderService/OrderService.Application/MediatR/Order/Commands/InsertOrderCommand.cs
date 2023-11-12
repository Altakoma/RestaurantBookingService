using MediatR;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Command;

namespace OrderService.Application.MediatR.Order.Commands
{
    public class InsertOrderCommand : Transactional, IRequest<ReadOrderDTO>
    {
        public int BookingId { get; set; }
        public int ClientId { get; set; }
        public int MenuId { get; set; }
    }
}
