using AutoMapper;
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
        private readonly IMapper _mapper;

        public UpdateClientHandler(ISqlClientRepository sqlClientRepository,
            IMapper mapper)
        {
            _sqlClientRepository = sqlClientRepository;
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

            return readClientDTO;
        }
    }
}
