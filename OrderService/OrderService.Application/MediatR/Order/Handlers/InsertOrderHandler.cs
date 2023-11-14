using AutoMapper;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.GrpcServices;
using OrderService.Application.Interfaces.Repositories.NoSql;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Application.MediatR.Order.Commands;
using OrderService.Application.TokenParsers.Interfaces;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.MediatR.Order.Handlers
{
    public class InsertOrderHandler : IRequestHandler<InsertOrderCommand, ReadOrderDTO>
    {
        private readonly ISqlOrderRepository _sqlOrderRepository;
        private readonly INoSqlOrderRepository _noSqlOrderRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IMapper _mapper;
        private readonly IGrpcClientBookingService _grpcClientBookingService;
        private readonly ITokenParser _tokenParser;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InsertOrderHandler(ISqlOrderRepository sqlClientRepository,
            INoSqlOrderRepository noSqlClientRepository,
            IBackgroundJobClient backgroundJobClient,
            IGrpcClientBookingService grpcClientBookingService,
            ITokenParser tokenParser,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _sqlOrderRepository = sqlClientRepository;
            _noSqlOrderRepository = noSqlClientRepository;
            _backgroundJobClient = backgroundJobClient;
            _grpcClientBookingService = grpcClientBookingService;
            _tokenParser = tokenParser;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ReadOrderDTO> Handle(InsertOrderCommand request,
            CancellationToken cancellationToken)
        {
            if (!request.IsRequestedBySystem)
            {
                int subjectId = _tokenParser
                    .ParseSubjectId(_httpContextAccessor?.HttpContext?.Request.Headers);

                request.ClientId = subjectId;

                var isClientBookedTableRequest = new IsClientBookedTableRequest
                {
                    ClientId = subjectId,
                    BookingId = request.BookingId,
                };

                IsClientBookedTableReply reply =
                    await _grpcClientBookingService.IsClientBookedTable(
                          isClientBookedTableRequest, cancellationToken);

                if (!reply.IsClientBookedTable)
                {
                    throw new AuthorizationException(request.BookingId.ToString(),
                        ExceptionMessages.NotClientBookingMessage);
                }
            }

            var order = _mapper.Map<Domain.Entities.Order>(request);

            var readOrderDTO = await _sqlOrderRepository
                .InsertAsync<ReadOrderDTO>(order, cancellationToken);

            _backgroundJobClient.Enqueue(
                () => _noSqlOrderRepository.InsertAsync(readOrderDTO, cancellationToken));

            return readOrderDTO;
        }
    }
}
