using MediatR;

namespace OrderService.Application.MediatR.Table.Commands
{
    public class DeleteTableCommand : IRequest
    {
        public int Id { get; set; }
    }
}
