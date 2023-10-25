using MediatR;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.Interfaces.Repositories.Write;
using OrderService.Application.MediatR.Menu.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Menu.Handlers
{
    public class DeleteMenuHandler : IRequestHandler<DeleteMenuCommand>
    {
        private readonly IWriteMenuRepository _writeMenuRepository;
        private readonly IReadMenuRepository _readMenuRepository;

        public DeleteMenuHandler(IWriteMenuRepository writeMenuRepository,
            IReadMenuRepository readMenuRepository)
        {
            _writeMenuRepository = writeMenuRepository;
            _readMenuRepository = readMenuRepository;
        }

        public async Task Handle(DeleteMenuCommand request,
            CancellationToken cancellationToken)
        {
            Domain.Entities.Menu menu = await _readMenuRepository
                                                  .GetByIdAsync(request.Id, cancellationToken);

            if (menu is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Menu),
                    request.Id.ToString(), typeof(Domain.Entities.Menu));
            }

            await _writeMenuRepository.DeleteAsync(request.Id, cancellationToken);

            bool isDeleted = await _writeMenuRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteMenuCommand),
                    request.Id.ToString(), typeof(Domain.Entities.Menu));
            }
        }
    }
}
