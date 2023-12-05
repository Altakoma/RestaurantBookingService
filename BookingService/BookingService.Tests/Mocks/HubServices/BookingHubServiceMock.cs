using BookingService.Application.DTOs.Booking;
using BookingService.Application.Enums.HubMessages;
using BookingService.Application.Interfaces.HubServices;
using Moq;

namespace BookingService.Tests.Mocks.HubServices
{
    public class BookingHubServiceMock : Mock<IBookingHubService>
    {
        public BookingHubServiceMock MockSendBookingMessageAsync<TEntity>(
            HubMessageType messageType, TEntity entity)
        {
            Setup(hubService => 
                hubService.SendBookingMessageAsync(messageType, entity))
            .Verifiable();

            return this;
        }
    }
}
