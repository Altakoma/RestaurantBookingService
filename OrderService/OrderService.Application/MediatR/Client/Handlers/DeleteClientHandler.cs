using MediatR;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Application.MediatR.Client.Commands;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Client.Handlers
{
    public class DeleteClientHandler : IRequestHandler<DeleteClientCommand>
    {
        private readonly ISqlClientRepository _sqlRepository;

        public DeleteClientHandler(ISqlClientRepository sqlRepository)
        {
            _sqlRepository = sqlRepository;
        }

        public async Task Handle(DeleteClientCommand request,
            CancellationToken cancellationToken)
        {
            var client = await _sqlRepository
               .GetByIdAsync<Domain.Entities.Client>(request.Id, cancellationToken);

            if (client is null)
            {
                throw new NotFoundException(request.Id.ToString(),
                    typeof(Domain.Entities.Client));
            }

            await _sqlRepository.DeleteAsync(request.Id, cancellationToken);

            bool isDeleted = await _sqlRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteClientCommand),
                    request.Id.ToString(), typeof(Domain.Entities.Client));
            }
        }
    }
}
