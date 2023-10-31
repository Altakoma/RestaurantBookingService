using BookingService.Application;
using BookingService.Application.Interfaces.Repositories;
using Grpc.Core;

namespace BookingService.Infrastructure.Grpc.Services.Servers
{
    public class GrpcServerBookingService : BookingGrpcService.BookingGrpcServiceBase
    {
        private readonly IBookingRepository _bookingRepository;

        public GrpcServerBookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async override Task<IsClientBookedTableReply> IsClientBookedTable(
            IsClientBookedTableRequest request, ServerCallContext context)
        {
            bool isClientBookedTable = await _bookingRepository.IsClientBookedTableAsync(
                request.ClientId, request.TableId, context.CancellationToken);

            var isClientBookedTableReply = new IsClientBookedTableReply
            {
                IsClientBookedTable = isClientBookedTable,
            };

            return isClientBookedTableReply;
        }
    }
}
