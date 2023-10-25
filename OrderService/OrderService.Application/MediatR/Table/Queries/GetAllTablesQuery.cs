using MediatR;
using OrderService.Application.DTOs.Table;

namespace OrderService.Application.MediatR.Table.Queries
{
    public class GetAllTablesQuery : IRequest<ICollection<ReadTableDTO>>
    {
    }
}
