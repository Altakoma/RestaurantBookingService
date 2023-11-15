using Hangfire;
using MediatR;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Repositories.NoSql;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Application.MediatR.Order.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Order.Handlers
{
    public class UpdateOrderBySynchronizationHandler : IRequestHandler<UpdateOrderBySynchronizationCommand>
    {
        private readonly ISqlOrderRepository _sqlOrderRepository;
        private readonly INoSqlOrderRepository _noSqlOrderRepository;

        public UpdateOrderBySynchronizationHandler(ISqlOrderRepository sqlClientRepository,
            INoSqlOrderRepository noSqlClientRepository)
        {
            _sqlOrderRepository = sqlClientRepository;
            _noSqlOrderRepository = noSqlClientRepository;
        }

        public async Task Handle(UpdateOrderBySynchronizationCommand request,
            CancellationToken cancellationToken)
        {
            ReadOrderDTO? readOrderDTO = await _noSqlOrderRepository
                    .GetByIdAsync(request.Id, cancellationToken);

            if (readOrderDTO is null)
            {
                throw new NotFoundException(request.Id.ToString(),
                    typeof(Domain.Entities.Order));
            }

            readOrderDTO = await _sqlOrderRepository
                .GetByIdAsync<ReadOrderDTO>(request.Id, cancellationToken);

            await _noSqlOrderRepository.UpdateAsync(readOrderDTO, cancellationToken);
        }
    }
}
