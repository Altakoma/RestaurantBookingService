using MediatR;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Repositories.NoSql;
using OrderService.Application.MediatR.Order.Queries;

namespace OrderService.Application.MediatR.Order.Handlers
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, ICollection<ReadOrderDTO>>
    {
        private readonly INoSqlOrderRepository _noSqlOrderRepository;

        public GetAllOrdersHandler(INoSqlOrderRepository noSqlClientRepository)
        {
            _noSqlOrderRepository = noSqlClientRepository;
        }

        public async Task<ICollection<ReadOrderDTO>> Handle(GetAllOrdersQuery request,
            CancellationToken cancellationToken)
        {
            ICollection<ReadOrderDTO> readOrderDTOs =
                await _noSqlOrderRepository.GetAllAsync(request.SkipCount,
                request.SelectionAmount, cancellationToken);

            return readOrderDTOs;
        }
    }
}
