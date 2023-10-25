using MediatR;

namespace OrderService.Application.MediatR.Order.Commands
{
    public class DeleteOrderCommand : IRequest
    {
        public int Id { get; set; }
    }
}
