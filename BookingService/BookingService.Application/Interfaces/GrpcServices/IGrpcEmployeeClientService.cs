namespace BookingService.Application.Interfaces.GrpcServices
{
    public interface IGrpcEmployeeClientService
    {
        Task<IsWorkingAtRestaurantReply> EmployeeWorksAtRestaurant(
            IsWorkingAtRestaurantRequest request, CancellationToken cancellationToken);
    }
}
