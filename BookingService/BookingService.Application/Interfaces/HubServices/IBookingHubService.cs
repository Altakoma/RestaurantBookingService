using BookingService.Application.Enums.HubMessages;

namespace BookingService.Application.Interfaces.HubServices
{
    public interface IBookingHubService
    {
        Task SendBookingMessageAsync<T>(HubMessageType hubMessageType, T bookingDTO);
    }
}
