using AutoMapper;
using Hangfire;
using MediatR;
using OrderService.Application.DTOs.Client;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Application.MediatR.Client.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Client.Handlers
{
    public class InsertClientHandler : IRequestHandler<InsertClientCommand, ReadClientDTO>
    {
        private readonly ISqlClientRepository _sqlRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMapper _mapper;

        public InsertClientHandler(ISqlClientRepository sqlClientRepository,
            IBackgroundJobClient backgroundJobClient,
            IMapper mapper)
        {
            _sqlRepository = sqlClientRepository;
            _backgroundJobClient = backgroundJobClient;
            _mapper = mapper;
        }

        public async Task<ReadClientDTO> Handle(InsertClientCommand request,
            CancellationToken cancellationToken)
        {
            var client = _mapper.Map<Domain.Entities.Client>(request);

            await _sqlRepository.InsertAsync(client, cancellationToken);

            bool isInserted = await _sqlRepository
                .SaveChangesToDbAsync(cancellationToken);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertClientHandler.Handle),
                    request.Id.ToString(), typeof(Domain.Entities.Client));
            }

            var readClientDTO = _mapper.Map<ReadClientDTO>(client);

            return readClientDTO;
        }
    }
}
