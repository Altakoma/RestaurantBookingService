using BookingService.Application.DTOs.Booking;
using BookingService.Application.Interfaces.Services;
using BookingService.Tests.Mocks.Services.Base;
using Moq;

namespace BookingService.Tests.Mocks.Services
{
    public class BookingServiceMock : BaseServiceMock<IBookingService>
    {
        public BookingServiceMock MockInsertItemAsync(
            InsertBookingDTO insertItemDTO, ReadBookingDTO readItemDTO)
        {
            Setup(service => service.InsertAsync<ReadBookingDTO>(
                insertItemDTO, It.IsAny<CancellationToken>()))
            .ReturnsAsync(readItemDTO)
            .Verifiable();

            return this;
        }
    }
}
