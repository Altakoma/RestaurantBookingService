using AutoMapper;
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
        private readonly IMapper _mapper;

        public InsertClientHandler(ISqlClientRepository sqlClientRepository,
            IMapper mapper)
        {
            _sqlRepository = sqlClientRepository;
            _mapper = mapper;
        }

        public async Task<ReadClientDTO> Handle(InsertClientCommand request,
            CancellationToken cancellationToken)
        {
            var client = _mapper.Map<Domain.Entities.Client>(request);

            ReadClientDTO readClientDTO = await _sqlRepository
                .InsertAsync<ReadClientDTO>(client, cancellationToken);

            bool isInserted = await _sqlRepository
                .SaveChangesToDbAsync(cancellationToken);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertClientHandler.Handle),
                    request.Id.ToString(), typeof(Domain.Entities.Client));
            }

            return readClientDTO;
        }
    }
}
