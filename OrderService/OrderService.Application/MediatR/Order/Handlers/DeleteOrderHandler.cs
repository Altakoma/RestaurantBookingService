using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Repositories.NoSql;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Application.MediatR.Order.Commands;
using OrderService.Application.TokenParsers.Interfaces;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Order.Handlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly ISqlOrderRepository _sqlOrderRepository;
        private readonly INoSqlOrderRepository _noSqlOrderRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly ITokenParser _tokenParser;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteOrderHandler(ISqlOrderRepository sqlClientRepository,
            INoSqlOrderRepository noSqlClientRepository,
            IBackgroundJobClient backgroundJobClient,
            ITokenParser tokenParser,
            IHttpContextAccessor httpContextAccessor)
        {
            _sqlOrderRepository = sqlClientRepository;
            _noSqlOrderRepository = noSqlClientRepository;
            _backgroundJobClient = backgroundJobClient;
            _tokenParser = tokenParser;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Handle(DeleteOrderCommand request,
            CancellationToken cancellationToken)
        {
            ReadOrderDTO? orderDTO = await _noSqlOrderRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (orderDTO is null)
            {
                throw new NotFoundException(request.Id.ToString(),
                    typeof(Domain.Entities.Order));
            }

            int subjectId = _tokenParser
                .ParseSubjectId(_httpContextAccessor?.HttpContext?.Request.Headers);

            if (orderDTO.ReadClientDTO.Id != subjectId)
            {
                throw new AuthorizationException(orderDTO.ReadTableDTO.Id.ToString(),
                    ExceptionMessages.NotClientBookingMessage);
            }

            await _sqlOrderRepository.DeleteAsync(request.Id, cancellationToken);

            bool isDeleted = await _sqlOrderRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteOrderCommand),
                    request.Id.ToString(), typeof(Domain.Entities.Order));
            }

            _backgroundJobClient.Enqueue(
                () => _noSqlOrderRepository.DeleteAsync(orderDTO.Id, cancellationToken));
        }
    }
}
