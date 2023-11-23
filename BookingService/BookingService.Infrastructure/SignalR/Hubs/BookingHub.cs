using Microsoft.AspNetCore.SignalR;

namespace BookingService.Infrastructure.SignalR.Hubs
{
    public class BookingHub : Hub<IBookingClient>
    {
        public const string HubPattern = "/bookinghub";
    }
}
