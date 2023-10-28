using Hangfire;
using MediatR;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Repositories.NoSql;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Application.MediatR.Order.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Order.Handlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly ISqlOrderRepository _sqlOrderRepository;
        private readonly INoSqlOrderRepository _noSqlOrderRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public DeleteOrderHandler(ISqlOrderRepository sqlClientRepository,
            INoSqlOrderRepository noSqlClientRepository,
            IBackgroundJobClient backgroundJobClient)
        {
            _sqlOrderRepository = sqlClientRepository;
            _noSqlOrderRepository = noSqlClientRepository;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task Handle(DeleteOrderCommand request,
            CancellationToken cancellationToken)
        {
            ReadOrderDTO? orderDTO = await _noSqlOrderRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (orderDTO is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Order),
                    request.Id.ToString(), typeof(Domain.Entities.Order));
            }

            await _sqlOrderRepository.DeleteAsync(request.Id, cancellationToken);

            bool isDeleted = await _sqlOrderRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteOrderCommand),
                    request.Id.ToString(), typeof(Domain.Entities.Order));
            }

            _backgroundJobClient.Enqueue(
                () => _noSqlOrderRepository.DeleteAsync(orderDTO.Id, cancellationToken));
        }
    }
}
