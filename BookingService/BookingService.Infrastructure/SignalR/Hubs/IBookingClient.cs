namespace BookingService.Infrastructure.SignalR.Hubs
{
    public interface IBookingClient
    {
        Task GetBookingMessageAsync(string messageType, string message);
    }
}
