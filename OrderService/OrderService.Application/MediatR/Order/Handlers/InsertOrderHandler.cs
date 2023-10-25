using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.Interfaces.Repositories.Write;
using OrderService.Application.MediatR.Order.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Order.Handlers
{
    public class InsertOrderHandler : IRequestHandler<InsertOrderCommand, ReadOrderDTO>
    {
        private readonly IWriteOrderRepository _writeOrderRepository;
        private readonly IReadOrderRepository _readOrderRepository;
        private readonly IMapper _mapper;

        public InsertOrderHandler(IWriteOrderRepository writeOrderRepository,
            IReadOrderRepository readOrderRepository, IMapper mapper)
        {
            _writeOrderRepository = writeOrderRepository;
            _readOrderRepository = readOrderRepository;
            _mapper = mapper;
        }

        public async Task<ReadOrderDTO> Handle(InsertOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Domain.Entities.Order>(request);

            await _writeOrderRepository.InsertAsync(order, cancellationToken);

            bool isInserted = await _writeOrderRepository
                .SaveChangesToDbAsync(cancellationToken);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertOrderHandler.Handle),
                    nameof(InsertOrderCommand), typeof(Domain.Entities.Order));
            }

            var readOrderDTO = _mapper.Map<ReadOrderDTO>(order);

            return readOrderDTO;
        }
    }
}
