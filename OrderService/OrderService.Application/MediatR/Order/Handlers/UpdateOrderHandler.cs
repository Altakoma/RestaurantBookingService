using AutoMapper;
using Hangfire;
using MediatR;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Repositories.NoSql;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Application.MediatR.Order.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Order.Handlers
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, ReadOrderDTO>
    {
        private readonly ISqlOrderRepository _sqlOrderRepository;
        private readonly INoSqlOrderRepository _noSqlOrderRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMapper _mapper;

        public UpdateOrderHandler(ISqlOrderRepository sqlClientRepository,
            INoSqlOrderRepository noSqlClientRepository,
            IBackgroundJobClient backgroundJobClient,
            IMapper mapper)
        {
            _sqlOrderRepository = sqlClientRepository;
            _noSqlOrderRepository = noSqlClientRepository;
            _backgroundJobClient = backgroundJobClient;
            _mapper = mapper;
        }

        public async Task<ReadOrderDTO> Handle(UpdateOrderCommand request,
            CancellationToken cancellationToken)
        {
            ReadOrderDTO? readOrderDTO = await _noSqlOrderRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (readOrderDTO is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Order),
                    request.Id.ToString(), typeof(Domain.Entities.Order));
            }

            var order = _mapper.Map<Domain.Entities.Order>(request);

            _sqlOrderRepository.Update(order);

            bool isUpdated = await _sqlOrderRepository.
                                   SaveChangesToDbAsync(cancellationToken);

            readOrderDTO = await _sqlOrderRepository
                .GetByIdAsync<ReadOrderDTO>(order.Id, cancellationToken);

            if (!isUpdated || readOrderDTO is null)
            {
                throw new DbOperationException(nameof(UpdateOrderHandler.Handle),
                    request.Id.ToString(), typeof(Domain.Entities.Order));
            }

            _backgroundJobClient.Enqueue(
                () => _noSqlOrderRepository.UpdateAsync(readOrderDTO, cancellationToken));

            return readOrderDTO;
        }
    }
}
