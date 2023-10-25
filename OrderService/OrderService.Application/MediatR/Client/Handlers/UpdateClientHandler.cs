using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Client;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.Interfaces.Repositories.Write;
using OrderService.Application.MediatR.Client.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Client.Handlers
{
    public class UpdateClientHandler : IRequestHandler<UpdateClientCommand, ReadClientDTO>
    {
        private readonly IWriteClientRepository _writeClientRepository;
        private readonly IReadClientRepository _readClientRepository;
        private readonly IMapper _mapper;

        public UpdateClientHandler(IWriteClientRepository writeClientRepository,
            IReadClientRepository readClientRepository, IMapper mapper)
        {
            _writeClientRepository = writeClientRepository;
            _readClientRepository = readClientRepository;
            _mapper = mapper;
        }

        public async Task<ReadClientDTO> Handle(UpdateClientCommand request,
            CancellationToken cancellationToken)
        {
            Domain.Entities.Client client = await _readClientRepository
                                                  .GetByIdAsync(request.Id, cancellationToken);

            if (client is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Client),
                    request.Id.ToString(), typeof(Domain.Entities.Client));
            }

            client = _mapper.Map<Domain.Entities.Client>(request);

            _writeClientRepository.Update(client);

            bool isUpdated = await _writeClientRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isUpdated)
            {
                throw new DbOperationException(nameof(UpdateClientHandler.Handle),
                    request.Id.ToString(), typeof(Domain.Entities.Client));
            }

            var readClientDTO = _mapper.Map<ReadClientDTO>(client);

            return readClientDTO;
        }
    }
}
