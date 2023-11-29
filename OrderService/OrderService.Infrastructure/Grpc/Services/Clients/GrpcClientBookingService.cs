using OrderService.Application;
using OrderService.Application.Interfaces.GrpcServices;

namespace OrderService.Infrastructure.Grpc.Services.Clients
{
    public class GrpcClientBookingService : IGrpcClientBookingService
    {
        private readonly BookingGrpcService.BookingGrpcServiceClient _bookingGrpcServiceClient;

        public GrpcClientBookingService(
            BookingGrpcService.BookingGrpcServiceClient bookingGrpcServiceClient)
        {
            _bookingGrpcServiceClient = bookingGrpcServiceClient;
        }

        public async Task<IsClientBookedTableReply> IsClientBookedTable(
            IsClientBookedTableRequest request, CancellationToken cancellationToken)
        {
            var reply = await _bookingGrpcServiceClient.IsClientBookedTableAsync(request,
                cancellationToken: cancellationToken);

            return reply;
        }
    }
}
