using MediatR;
using OrderService.Application.DTOs.Menu;

namespace OrderService.Application.MediatR.Menu.Queries
{
    public class GetAllMenuQuery : IRequest<ICollection<ReadMenuDTO>>
    {
    }
}
