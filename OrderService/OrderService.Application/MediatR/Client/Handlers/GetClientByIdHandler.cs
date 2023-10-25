using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Client;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.MediatR.Client.Queries;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Client.Handlers
{
    public class GetClientByIdHandler : IRequestHandler<GetClientByIdQuery, ReadClientDTO>
    {
        private readonly IReadClientRepository _readClientRepository;
        private readonly IMapper _mapper;

        public GetClientByIdHandler(IReadClientRepository readClientRepository,
            IMapper mapper)
        {
            _readClientRepository = readClientRepository;
            _mapper = mapper;
        }

        public async Task<ReadClientDTO> Handle(GetClientByIdQuery request,
            CancellationToken cancellationToken)
        {
            Domain.Entities.Client client = await _readClientRepository
                                                  .GetByIdAsync(request.Id, cancellationToken);

            if (client is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Client),
                    request.Id.ToString(), typeof(Domain.Entities.Client));
            }

            var readClientDTO = _mapper.Map<ReadClientDTO>(client);

            return readClientDTO;
        }
    }
}
