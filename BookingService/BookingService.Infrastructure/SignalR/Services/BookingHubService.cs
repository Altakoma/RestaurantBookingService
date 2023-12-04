using BookingService.Application.Enums.HubMessages;
using BookingService.Application.Interfaces.HubServices;
using BookingService.Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace BookingService.Infrastructure.SignalR.Services
{
    public class BookingHubService : IBookingHubService
    {
        private readonly IHubContext<BookingHub, IBookingClient> _hubContext;

        public BookingHubService(IHubContext<BookingHub, IBookingClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendBookingMessageAsync<T>(HubMessageType hubMessageType, T bookingDTO)
        {
            string messageType = hubMessageType.ToString();
            string jsonData = JsonSerializer.Serialize(bookingDTO);

            await _hubContext.Clients.All.GetBookingMessageAsync(messageType, jsonData);
        }
    }
}
