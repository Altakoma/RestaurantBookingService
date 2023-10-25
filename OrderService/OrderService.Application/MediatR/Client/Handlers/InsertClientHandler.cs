using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Client;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.Interfaces.Repositories.Write;
using OrderService.Application.MediatR.Client.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Client.Handlers
{
    public class InsertClientHandler : IRequestHandler<InsertClientCommand, ReadClientDTO>
    {
        private readonly IWriteClientRepository _writeClientRepository;
        private readonly IReadClientRepository _readClientRepository;
        private readonly IMapper _mapper;

        public InsertClientHandler(IWriteClientRepository writeClientRepository,
            IReadClientRepository readClientRepository, IMapper mapper)
        {
            _writeClientRepository = writeClientRepository;
            _readClientRepository = readClientRepository;
            _mapper = mapper;
        }

        public async Task<ReadClientDTO> Handle(InsertClientCommand request,
            CancellationToken cancellationToken)
        {
            var client = _mapper.Map<Domain.Entities.Client>(request);

            await _writeClientRepository.InsertAsync(client, cancellationToken);

            bool isInserted = await _writeClientRepository
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
