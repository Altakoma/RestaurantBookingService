using MediatR;
using OrderService.Application.DTOs.Client;

namespace OrderService.Application.MediatR.Client.Queries
{
    public class GetAllClientsQuery : IRequest<ICollection<ReadClientDTO>>
    {
    }
}
