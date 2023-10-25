using MediatR;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.Interfaces.Repositories.Write;
using OrderService.Application.MediatR.Order.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Order.Handlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IWriteOrderRepository _writeOrderRepository;
        private readonly IReadOrderRepository _readOrderRepository;

        public DeleteOrderHandler(IWriteOrderRepository writeOrderRepository,
            IReadOrderRepository readOrderRepository)
        {
            _writeOrderRepository = writeOrderRepository;
            _readOrderRepository = readOrderRepository;
        }

        public async Task Handle(DeleteOrderCommand request,
            CancellationToken cancellationToken)
        {
            Domain.Entities.Order order = await _readOrderRepository
                                                  .GetByIdAsync(request.Id, cancellationToken);

            if (order is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Order),
                    request.Id.ToString(), typeof(Domain.Entities.Order));
            }

            await _writeOrderRepository.DeleteAsync(request.Id, cancellationToken);

            bool isDeleted = await _writeOrderRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteOrderCommand),
                    request.Id.ToString(), typeof(Domain.Entities.Order));
            }
        }
    }
}
