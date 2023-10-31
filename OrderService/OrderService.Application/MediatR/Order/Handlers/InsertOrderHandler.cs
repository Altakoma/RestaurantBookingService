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
    public class InsertOrderHandler : IRequestHandler<InsertOrderCommand, ReadOrderDTO>
    {
        private readonly ISqlOrderRepository _sqlOrderRepository;
        private readonly INoSqlOrderRepository _noSqlOrderRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMapper _mapper;

        public InsertOrderHandler(ISqlOrderRepository sqlClientRepository,
            INoSqlOrderRepository noSqlClientRepository,
            IBackgroundJobClient backgroundJobClient,
            IMapper mapper)
        {
            _sqlOrderRepository = sqlClientRepository;
            _noSqlOrderRepository = noSqlClientRepository;
            _backgroundJobClient = backgroundJobClient;
            _mapper = mapper;
        }

        public async Task<ReadOrderDTO> Handle(InsertOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Domain.Entities.Order>(request);

            var readOrderDTO = await _sqlOrderRepository
                .InsertAsync<ReadOrderDTO>(order, cancellationToken);

            bool isInserted = await _sqlOrderRepository
                .SaveChangesToDbAsync(cancellationToken);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertOrderHandler.Handle),
                    nameof(InsertOrderCommand), typeof(Domain.Entities.Order));
            }

            _backgroundJobClient.Enqueue(
                () => _noSqlOrderRepository.InsertAsync(readOrderDTO, cancellationToken));

            return readOrderDTO;
        }
    }
}
