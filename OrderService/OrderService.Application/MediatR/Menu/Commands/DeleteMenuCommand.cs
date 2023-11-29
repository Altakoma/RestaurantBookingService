using MediatR;
using OrderService.Application.Interfaces.Command;

namespace OrderService.Application.MediatR.Menu.Commands
{
    public class DeleteMenuCommand : IRequest, ITransactional
    {
        public int Id { get; set; }
    }
}
