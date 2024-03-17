using Grpc.Core;

namespace OrderService.Application.Interfaces.GrpcServices
{
    public interface IGrpcClientBookingService
    {
        Task<IsClientBookedTableReply> IsClientBookedTable(
            IsClientBookedTableRequest request, CancellationToken cancellationToken);
    }
}
