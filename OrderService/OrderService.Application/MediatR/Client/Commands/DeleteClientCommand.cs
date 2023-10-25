using MediatR;

namespace OrderService.Application.MediatR.Client.Commands
{
    public class DeleteClientCommand : IRequest
    {
        public int Id { get; set; }
    }
}
