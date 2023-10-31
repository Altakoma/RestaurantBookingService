using Hangfire;
using MediatR;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Application.MediatR.Table.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Table.Handlers
{
    public class DeleteTableHandler : IRequestHandler<DeleteTableCommand>
    {
        private readonly ISqlTableRepository _sqlTableRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public DeleteTableHandler(ISqlTableRepository sqlTableRepository,
            IBackgroundJobClient backgroundJobClient)
        {
            _sqlTableRepository = sqlTableRepository;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task Handle(DeleteTableCommand request,
            CancellationToken cancellationToken)
        {
            var table = await _sqlTableRepository
                .GetByIdAsync<Domain.Entities.Table>(request.Id, cancellationToken);

            if (table is null)
            {
                throw new NotFoundException(request.Id.ToString(),
                    typeof(Domain.Entities.Table));
            }

            await _sqlTableRepository.DeleteAsync(request.Id, cancellationToken);

            bool isDeleted = await _sqlTableRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteTableCommand),
                    request.Id.ToString(), typeof(Domain.Entities.Table));
            }
        }
    }
}
