using MediatR;
using OrderService.Application.DTOs.Menu;

namespace OrderService.Application.MediatR.Menu.Queries
{
    public class GetMenuByIdQuery : IRequest<ReadMenuDTO>
    {
        public int Id { get; set; }
    }
}
