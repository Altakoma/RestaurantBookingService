using AutoMapper;
using MediatR;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Repositories.NoSql;
using OrderService.Application.MediatR.Order.Queries;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Order.Handlers
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, ReadOrderDTO>
    {
        private readonly INoSqlOrderRepository _noSqlOrderRepository;

        public GetOrderByIdHandler(INoSqlOrderRepository noSqlClientRepository)
        {
            _noSqlOrderRepository = noSqlClientRepository;
        }

        public async Task<ReadOrderDTO> Handle(GetOrderByIdQuery request,
            CancellationToken cancellationToken)
        {
            ReadOrderDTO? readOrderDTO = await _noSqlOrderRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (readOrderDTO is null)
            {
                throw new NotFoundException(request.Id.ToString(),
                    typeof(Domain.Entities.Order));
            }

            return readOrderDTO;
        }
    }
}
