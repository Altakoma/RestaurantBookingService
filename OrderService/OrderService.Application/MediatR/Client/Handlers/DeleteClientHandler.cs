using MediatR;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.Interfaces.Repositories.Write;
using OrderService.Application.MediatR.Client.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Client.Handlers
{
    public class DeleteClientHandler : IRequestHandler<DeleteClientCommand>
    {
        private readonly IWriteClientRepository _writeClientRepository;
        private readonly IReadClientRepository _readClientRepository;

        public DeleteClientHandler(IWriteClientRepository writeClientRepository,
            IReadClientRepository readClientRepository)
        {
            _writeClientRepository = writeClientRepository;
            _readClientRepository = readClientRepository;
        }

        public async Task Handle(DeleteClientCommand request,
            CancellationToken cancellationToken)
        {
            Domain.Entities.Client client = await _readClientRepository
                                                  .GetByIdAsync(request.Id, cancellationToken);

            if (client is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Client),
                    request.Id.ToString(), typeof(Domain.Entities.Client));
            }

            await _writeClientRepository.DeleteAsync(request.Id, cancellationToken);

            bool isDeleted = await _writeClientRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteClientCommand),
                    request.Id.ToString(), typeof(Domain.Entities.Client));
            }
        }
    }
}
