using AutoMapper;
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
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, ReadOrderDTO>
    {
        private readonly ISqlOrderRepository _sqlOrderRepository;
        private readonly INoSqlOrderRepository _noSqlOrderRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMapper _mapper;
        private readonly ITokenParser _tokenParser;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateOrderHandler(ISqlOrderRepository sqlClientRepository,
            INoSqlOrderRepository noSqlClientRepository,
            IBackgroundJobClient backgroundJobClient,
            ITokenParser tokenParser,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _sqlOrderRepository = sqlClientRepository;
            _noSqlOrderRepository = noSqlClientRepository;
            _backgroundJobClient = backgroundJobClient;
            _httpContextAccessor = httpContextAccessor;
            _tokenParser = tokenParser;
            _mapper = mapper;
        }

        public async Task<ReadOrderDTO> Handle(UpdateOrderCommand request,
            CancellationToken cancellationToken)
        {
            int subjectId = _tokenParser
                .ParseSubjectId(_httpContextAccessor?.HttpContext?.Request.Headers);

            request.ClientId = subjectId;

            ReadOrderDTO? readOrderDTO = await _noSqlOrderRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (readOrderDTO is null)
            {
                throw new NotFoundException(request.Id.ToString(),
                    typeof(Domain.Entities.Order));
            }

            if (readOrderDTO.ReadClientDTO.Id != subjectId)
            {
                throw new AuthorizationException(readOrderDTO.BookingId.ToString(),
                    ExceptionMessages.NotClientBookingMessage);
            }

            readOrderDTO.ReadMenuDTO = default!;

            var order = _mapper.Map<Domain.Entities.Order>(readOrderDTO);

            _mapper.Map(request, order);

            readOrderDTO = await _sqlOrderRepository
                .UpdateAsync<ReadOrderDTO>(order, cancellationToken);

            _backgroundJobClient.Enqueue(
                () => _noSqlOrderRepository.UpdateAsync(readOrderDTO, cancellationToken));

            return readOrderDTO;
        }
    }
}
