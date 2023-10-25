using MediatR;
using OrderService.Application.DTOs.Table;

namespace OrderService.Application.MediatR.Table.Commands
{
    public class UpdateTableCommand : IRequest<ReadTableDTO>
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
    }
}
