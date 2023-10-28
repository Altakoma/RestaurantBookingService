using MediatR;
using OrderService.Application.DTOs.Table;
using OrderService.Application.Interfaces.Command;

namespace OrderService.Application.MediatR.Table.Commands
{
    public class InsertTableCommand : IRequest<ReadTableDTO>, ITransactional
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
    }
}
