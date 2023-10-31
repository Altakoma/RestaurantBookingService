using AutoMapper;
using Hangfire;
using MediatR;
using OrderService.Application.DTOs.Client;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Application.MediatR.Client.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Client.Handlers
{
    public class UpdateClientHandler : IRequestHandler<UpdateClientCommand, ReadClientDTO>
    {
        private readonly ISqlClientRepository _sqlClientRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMapper _mapper;

        public UpdateClientHandler(ISqlClientRepository sqlClientRepository,
            IBackgroundJobClient backgroundJobClient,
            IMapper mapper)
        {
            _sqlClientRepository = sqlClientRepository;
            _backgroundJobClient = backgroundJobClient;
            _mapper = mapper;
        }

        public async Task<ReadClientDTO> Handle(UpdateClientCommand request,
            CancellationToken cancellationToken)
        {
            ReadClientDTO? readClientDTO = await _sqlClientRepository
                .GetByIdAsync<ReadClientDTO>(request.Id, cancellationToken);

            if (readClientDTO is null)
            {
                throw new NotFoundException(request.Id.ToString(),
                    typeof(Domain.Entities.Client));
            }

            var client = _mapper.Map<Domain.Entities.Client>(request);

            readClientDTO = await _sqlClientRepository
                .UpdateAsync<ReadClientDTO>(client, cancellationToken);

            bool isUpdated = await _sqlClientRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            readClientDTO = await _sqlClientRepository
                .GetByIdAsync<ReadClientDTO>(client.Id, cancellationToken);

            if (!isUpdated)
            {
                throw new DbOperationException(nameof(UpdateClientHandler.Handle),
                    request.Id.ToString(), typeof(Domain.Entities.Client));
            }

            return readClientDTO;
        }
    }
}
