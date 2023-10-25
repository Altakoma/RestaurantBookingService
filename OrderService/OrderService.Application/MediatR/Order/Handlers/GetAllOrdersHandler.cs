using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.MediatR.Order.Queries;

namespace OrderService.Application.MediatR.Order.Handlers
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, ICollection<ReadOrderDTO>>
    {
        private readonly IReadOrderRepository _readOrderRepository;
        private readonly IMapper _mapper;

        public GetAllOrdersHandler(IReadOrderRepository readOrderRepository,
            IMapper mapper)
        {
            _readOrderRepository = readOrderRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<ReadOrderDTO>> Handle(GetAllOrdersQuery request,
            CancellationToken cancellationToken)
        {
            ICollection<Domain.Entities.Order> menu =
                await _readOrderRepository.GetAllAsync(cancellationToken);

            var readOrdersDTOs = _mapper.Map<ICollection<ReadOrderDTO>>(menu);

            return readOrdersDTOs;
        }
    }
}
