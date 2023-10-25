using MediatR;

namespace OrderService.Application.MediatR.Menu.Commands
{
    public class DeleteMenuCommand : IRequest
    {
        public int Id { get; set; }
    }
}
