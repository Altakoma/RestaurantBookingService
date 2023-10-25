using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Client;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.MediatR.Client.Queries;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Client.Handlers
{
    public class GetAllClientsHandler : IRequestHandler<GetAllClientsQuery, ICollection<ReadClientDTO>>
    {
        private readonly IReadClientRepository _readClientRepository;
        private readonly IMapper _mapper;

        public GetAllClientsHandler(IReadClientRepository readClientRepository,
            IMapper mapper)
        {
            _readClientRepository = readClientRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<ReadClientDTO>> Handle(GetAllClientsQuery request,
            CancellationToken cancellationToken)
        {
            ICollection<Domain.Entities.Client> clients = 
                await _readClientRepository.GetAllAsync(cancellationToken);

            var readClientDTOs = _mapper.Map<ICollection<ReadClientDTO>>(clients);

            return readClientDTOs;
        }
    }
}
