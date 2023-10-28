using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Repositories.NoSql;
using OrderService.Application.MediatR.Order.Queries;

namespace OrderService.Application.MediatR.Order.Handlers
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, ICollection<ReadOrderDTO>>
    {
        private readonly INoSqlOrderRepository _noSqlOrderRepository;
        private readonly IMapper _mapper;

        public GetAllOrdersHandler(INoSqlOrderRepository noSqlClientRepository,
            IMapper mapper)
        {
            _noSqlOrderRepository = noSqlClientRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<ReadOrderDTO>> Handle(GetAllOrdersQuery request,
            CancellationToken cancellationToken)
        {
            ICollection<ReadOrderDTO> readOrderDTOs =
                await _noSqlOrderRepository.GetAllAsync(cancellationToken);

            return readOrderDTOs;
        }
    }
}
