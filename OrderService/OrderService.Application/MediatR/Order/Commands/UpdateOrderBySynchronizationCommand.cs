using MediatR;

namespace OrderService.Application.MediatR.Order.Commands
{
    public class UpdateOrderBySynchronizationCommand : IRequest
    {
        public int Id { get; set; }
    }
}
