using MediatR;
using OrderService.Application.DTOs.Order;

namespace OrderService.Application.MediatR.Order.Queries
{
    public class GetAllOrdersQuery : IRequest<ICollection<ReadOrderDTO>>
    {
        public int SkipCount { get; set; }
        public int SelectionAmount { get; set; }
    }
}
