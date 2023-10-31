using AutoMapper;
using Hangfire;
using MediatR;
using OrderService.Application.DTOs.Table;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Application.MediatR.Table.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Table.Handlers
{
    public class UpdateTableHandler : IRequestHandler<UpdateTableCommand, ReadTableDTO>
    {
        private readonly ISqlTableRepository _sqlTableRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMapper _mapper;

        public UpdateTableHandler(ISqlTableRepository sqlTableRepository,
            IBackgroundJobClient backgroundJobClient,
            IMapper mapper)
        {
            _sqlTableRepository = sqlTableRepository;
            _backgroundJobClient = backgroundJobClient;
            _mapper = mapper;
        }

        public async Task<ReadTableDTO> Handle(UpdateTableCommand request,
            CancellationToken cancellationToken)
        {
            var table = await _sqlTableRepository
                .GetByIdAsync<Domain.Entities.Table>(request.Id, cancellationToken);

            if (table is null)
            {
                throw new NotFoundException(request.Id.ToString(),
                    typeof(Domain.Entities.Table));
            }

            table = _mapper.Map<Domain.Entities.Table>(request);

            _sqlTableRepository.Update(table);

            bool isUpdated = await _sqlTableRepository.
                                   SaveChangesToDbAsync(cancellationToken);

            if (!isUpdated)
            {
                throw new DbOperationException(nameof(UpdateTableHandler.Handle),
                    request.Id.ToString(), typeof(Domain.Entities.Table));
            }

            ReadTableDTO readTableDTO = await _sqlTableRepository
                .GetByIdAsync<ReadTableDTO>(table.Id, cancellationToken);

            return readTableDTO;
        }
    }
}
