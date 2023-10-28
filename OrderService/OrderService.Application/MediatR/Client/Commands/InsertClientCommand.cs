using MediatR;
using OrderService.Application.DTOs.Client;
using OrderService.Application.Interfaces.Command;

namespace OrderService.Application.MediatR.Client.Commands
{
    public class InsertClientCommand : IRequest<ReadClientDTO>, ITransactional
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
