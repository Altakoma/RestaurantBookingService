using MediatR;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.Interfaces.Repositories.Write;
using OrderService.Application.MediatR.Table.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Table.Handlers
{
    public class DeleteTableHandler : IRequestHandler<DeleteTableCommand>
    {
        private readonly IWriteTableRepository _writeTableRepository;
        private readonly IReadTableRepository _readTableRepository;

        public DeleteTableHandler(IWriteTableRepository writeTableRepository,
            IReadTableRepository readTableRepository)
        {
            _writeTableRepository = writeTableRepository;
            _readTableRepository = readTableRepository;
        }

        public async Task Handle(DeleteTableCommand request,
            CancellationToken cancellationToken)
        {
            Domain.Entities.Table table = 
                await _readTableRepository.GetByIdAsync(request.Id, cancellationToken);

            if (table is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Table),
                    request.Id.ToString(), typeof(Domain.Entities.Table));
            }

            await _writeTableRepository.DeleteAsync(request.Id, cancellationToken);

            bool isDeleted = await _writeTableRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteTableCommand),
                    request.Id.ToString(), typeof(Domain.Entities.Table));
            }
        }
    }
}
