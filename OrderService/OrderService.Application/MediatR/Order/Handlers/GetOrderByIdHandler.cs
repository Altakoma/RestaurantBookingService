using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.MediatR.Order.Queries;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Order.Handlers
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, ReadOrderDTO>
    {
        private readonly IReadOrderRepository _readOrderRepository;
        private readonly IMapper _mapper;

        public GetOrderByIdHandler(IReadOrderRepository readOrderRepository,
            IMapper mapper)
        {
            _readOrderRepository = readOrderRepository;
            _mapper = mapper;
        }

        public async Task<ReadOrderDTO> Handle(GetOrderByIdQuery request,
            CancellationToken cancellationToken)
        {
            Domain.Entities.Order order = await _readOrderRepository
                                                  .GetByIdAsync(request.Id, cancellationToken);

            if (order is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Order),
                    request.Id.ToString(), typeof(Domain.Entities.Order));
            }

            var readOrderDTO = _mapper.Map<ReadOrderDTO>(order);

            return readOrderDTO;
        }
    }
}
