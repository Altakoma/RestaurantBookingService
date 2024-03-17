using AutoMapper;
using BookingService.Application.DTOs.Booking;
using BookingService.Application.MappingProfiles;
using BookingService.Domain.Entities;
using BookingService.Presentation.Controllers;
using BookingService.Tests.Fakers;
using BookingService.Tests.Mocks.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookingService.Tests.ControllerTests
{
    public class BookingControllerTests
    {
        private readonly BookingServiceMock _bookingServiceMock;

        private readonly IMapper _mapper;
        private readonly BookingController _bookingController;

        public BookingControllerTests()
        {
            _bookingServiceMock = new();

            _mapper = new Mapper(new MapperConfiguration(configure =>
                configure.AddProfile(new BookingProfile())));

            _bookingController = new BookingController(_bookingServiceMock.Object);
        }

        [Fact]
        public async Task GetAllBookingsAsync_ReturnsReadBookingDTOs()
        {
            //Arrange
            Booking booking = BookingDataFaker.GetFakedBooking();
            var bookings = new List<Booking> { booking };
            var readBookingDTOs = _mapper.Map<List<ReadBookingDTO>>(bookings);

            _bookingServiceMock.MockGetAllAsync(readBookingDTOs);

            //Act
            var result = await _bookingController.GetAllBookingsAsync(It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(readBookingDTOs);
        }

        [Fact]
        public async Task GetBookingAsync_ReturnsReadBookingDTO()
        {
            //Arrange
            Booking booking = BookingDataFaker.GetFakedBooking();
            var readBookingDTO = _mapper.Map<ReadBookingDTO>(booking);

            _bookingServiceMock.MockGetItemAsync(readBookingDTO);

            //Act
            var result = await _bookingController.GetBookingAsync(booking.Id,
                It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(readBookingDTO);
        }

        [Fact]
        public async Task InsertBookingAsync_ReturnsReadBookingDTO()
        {
            //Arrange
            Booking booking = BookingDataFaker.GetFakedBooking();
            var insertBookingDTO = _mapper.Map<InsertBookingDTO>(booking);
            var readBookingDTO = _mapper.Map<ReadBookingDTO>(booking);

            _bookingServiceMock.MockInsertItemAsync(insertBookingDTO,
                readBookingDTO);

            //Act
            var result = await _bookingController.InsertBookingAsync(insertBookingDTO,
                It.IsAny<CancellationToken>());
            var createdAtActionResult = result as CreatedAtActionResult;

            //Assert
            result.Should().BeOfType(typeof(CreatedAtActionResult));

            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult!.Value.Should().BeEquivalentTo(readBookingDTO);
        }

        [Fact]
        public async Task DeleteBookingAsync_ReturnsNoContent()
        {
            //Arrange
            Booking booking = BookingDataFaker.GetFakedBooking();

            _bookingServiceMock.MockDeleteItemAsync(booking.Id);

            //Act
            var result = await _bookingController.DeleteBookingAsync(booking.Id,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeOfType(typeof(NoContentResult));
        }
    }
}
