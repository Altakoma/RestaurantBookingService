using MediatR;
using OrderService.Application.DTOs.Client;

namespace OrderService.Application.MediatR.Client.Queries
{
    public class GetClientByIdQuery : IRequest<ReadClientDTO>
    {
        public int Id { get; set; }
    }
}
