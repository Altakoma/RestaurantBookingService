using MediatR;
using OrderService.Application.Interfaces.Command;

namespace OrderService.Application.MediatR.Client.Commands
{
    public class DeleteClientCommand : Transactional, IRequest
    {
        public int Id { get; set; }
    }
}
