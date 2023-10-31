using AutoMapper;
using Hangfire;
using MediatR;
using OrderService.Application.DTOs.Table;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Application.MediatR.Table.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Table.Handlers
{
    public class InsertTableHandler : IRequestHandler<InsertTableCommand, ReadTableDTO>
    {
        private readonly ISqlTableRepository _sqlTableRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMapper _mapper;

        public InsertTableHandler(ISqlTableRepository sqlTableRepository,
            IBackgroundJobClient backgroundJobClient,
            IMapper mapper)
        {
            _sqlTableRepository = sqlTableRepository;
            _backgroundJobClient = backgroundJobClient;
            _mapper = mapper;
        }

        public async Task<ReadTableDTO> Handle(InsertTableCommand request,
            CancellationToken cancellationToken)
        {
            var table = _mapper.Map<Domain.Entities.Table>(request);

            ReadTableDTO readTableDTO = 
                await _sqlTableRepository.InsertAsync<ReadTableDTO>(table, cancellationToken);

            bool isInserted = await _sqlTableRepository
                .SaveChangesToDbAsync(cancellationToken);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertTableHandler.Handle),
                    request.Id.ToString(), typeof(Domain.Entities.Table));
            }

            return readTableDTO;
        }
    }
}
