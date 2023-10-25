using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.Interfaces.Repositories.Write;
using OrderService.Application.MediatR.Order.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Order.Handlers
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, ReadOrderDTO>
    {
        private readonly IWriteOrderRepository _writeOrderRepository;
        private readonly IReadOrderRepository _readOrderRepository;
        private readonly IMapper _mapper;

        public UpdateOrderHandler(IWriteOrderRepository writeOrderRepository,
            IReadOrderRepository readOrderRepository, IMapper mapper)
        {
            _writeOrderRepository = writeOrderRepository;
            _readOrderRepository = readOrderRepository;
            _mapper = mapper;
        }

        public async Task<ReadOrderDTO> Handle(UpdateOrderCommand request,
            CancellationToken cancellationToken)
        {
            Domain.Entities.Order order = await _readOrderRepository.GetByIdAsync(request.Id, cancellationToken);

            if (order is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Order),
                    request.Id.ToString(), typeof(Domain.Entities.Order));
            }

            order = _mapper.Map<Domain.Entities.Order>(request);

            _writeOrderRepository.Update(order);

            bool isUpdated = await _writeOrderRepository.
                                   SaveChangesToDbAsync(cancellationToken);

            if (!isUpdated)
            {
                throw new DbOperationException(nameof(UpdateOrderHandler.Handle),
                    request.Id.ToString(), typeof(Domain.Entities.Order));
            }

            var readOrderDTO = _mapper.Map<ReadOrderDTO>(order);

            return readOrderDTO;
        }
    }
}
