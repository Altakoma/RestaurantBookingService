using MediatR;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Command;

namespace OrderService.Application.MediatR.Order.Commands
{
    public class UpdateOrderCommand : IRequest<ReadOrderDTO>, ITransactional
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int ClientId { get; set; }
        public int TableId { get; set; }
        public int MenuId { get; set; }
    }
}
