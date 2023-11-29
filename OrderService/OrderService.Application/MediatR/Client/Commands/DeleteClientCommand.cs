using MediatR;
using OrderService.Application.Interfaces.Command;

namespace OrderService.Application.MediatR.Client.Commands
{
    public class DeleteClientCommand : IRequest, ITransactional
    {
        public int Id { get; set; }
    }
}
