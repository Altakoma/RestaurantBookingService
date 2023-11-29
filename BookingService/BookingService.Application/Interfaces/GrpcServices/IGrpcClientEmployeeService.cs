namespace BookingService.Application.Interfaces.GrpcServices
{
    public interface IGrpcClientEmployeeService
    {
        Task<IsWorkingAtRestaurantReply> IsEmployeeWorkingAtRestaurant(
            IsWorkingAtRestaurantRequest request, CancellationToken cancellationToken);
    }
}
