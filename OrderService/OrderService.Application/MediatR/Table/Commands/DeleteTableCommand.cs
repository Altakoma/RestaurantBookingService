using MediatR;
using OrderService.Application.Interfaces.Command;

namespace OrderService.Application.MediatR.Table.Commands
{
    public class DeleteTableCommand : IRequest, ITransactional
    {
        public int Id { get; set; }
    }
}
