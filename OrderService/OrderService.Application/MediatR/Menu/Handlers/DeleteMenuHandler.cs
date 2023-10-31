using Hangfire;
using MediatR;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Application.MediatR.Menu.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Menu.Handlers
{
    public class DeleteMenuHandler : IRequestHandler<DeleteMenuCommand>
    {
        private readonly ISqlMenuRepository _sqlMenuRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public DeleteMenuHandler(ISqlMenuRepository sqlClientRepository,
            IBackgroundJobClient backgroundJobClient)
        {
            _sqlMenuRepository = sqlClientRepository;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task Handle(DeleteMenuCommand request,
            CancellationToken cancellationToken)
        {
            var menu = await _sqlMenuRepository
                .GetByIdAsync<Domain.Entities.Menu>(request.Id, cancellationToken);

            if (menu is null)
            {
                throw new NotFoundException(request.Id.ToString(),
                    typeof(Domain.Entities.Menu));
            }

            await _sqlMenuRepository.DeleteAsync(request.Id, cancellationToken);

            bool isDeleted = await _sqlMenuRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteMenuCommand),
                    request.Id.ToString(), typeof(Domain.Entities.Menu));
            }
        }
    }
}
