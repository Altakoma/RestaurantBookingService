namespace BookingService.Application.Interfaces.GrpcServices
{
    public interface IGrpcClientEmployeeService
    {
        Task<IsWorkingAtRestaurantReply> EmployeeWorksAtRestaurant(
            IsWorkingAtRestaurantRequest request, CancellationToken cancellationToken);
    }
}
