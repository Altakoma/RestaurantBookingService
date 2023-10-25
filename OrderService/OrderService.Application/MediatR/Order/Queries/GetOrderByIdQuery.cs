using MediatR;
using OrderService.Application.DTOs.Order;

namespace OrderService.Application.MediatR.Order.Queries
{
    public class GetOrderByIdQuery : IRequest<ReadOrderDTO>
    {
        public int Id { get; set; }
    }
}
