using MediatR;
using OrderService.Application.DTOs.Order;

namespace OrderService.Application.MediatR.Order.Queries
{
    public class GetAllOrdersQuery : IRequest<ICollection<ReadOrderDTO>>
    {
    }
}
