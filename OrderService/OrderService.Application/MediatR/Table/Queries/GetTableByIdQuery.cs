using MediatR;
using OrderService.Application.DTOs.Table;

namespace OrderService.Application.MediatR.Table.Queries
{
    public class GetTableByIdQuery : IRequest<ReadTableDTO>
    {
        public int Id { get; set; }
    }
}
