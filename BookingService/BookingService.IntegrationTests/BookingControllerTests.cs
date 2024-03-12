using AutoMapper;
using BookingService.Application.DTOs.Booking;
using BookingService.Application.Enums.HubMessages;
using BookingService.Domain.Entities;
using BookingService.Infrastructure.Data.ApplicationDbContext;
using BookingService.Presentation.Controllers;
using BookingService.Tests.Fakers;
using BookingService.Tests.Mocks.HubServices;
using BookingService.Tests.Mocks.TokenParsers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.IntegrationTests
{
    public class BookingControllerTests : IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly IServiceScope _serviceScope;

        private readonly TokenParserMock _tokenParserMock;
        private readonly BookingHubServiceMock _bookingHubServiceMock;

        private readonly BookingServiceDbContext _dbContext;
        private readonly IMapper _mapper;

        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly BookingController _bookingController;

        public BookingControllerTests(IntegrationTestWebAppFactory factory)
        {
            _serviceScope = factory.Services.CreateScope();

            _tokenParserMock = factory.TokenParserMock;
            _bookingHubServiceMock = factory.BookingHubServiceMock;

            _dbContext = _serviceScope.ServiceProvider
               .GetRequiredService<BookingServiceDbContext>();

            _mapper = _serviceScope.ServiceProvider
                .GetRequiredService<IMapper>();

            _cancellationTokenSource = new CancellationTokenSource();

            _bookingController = _serviceScope.ServiceProvider
                .GetRequiredService<BookingController>();
        }

        [Fact]
        public async Task GetAllBookingsAsync_ReturnsReadBookingDTOs()
        {
            //Arrange
            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _bookingController
                .GetAllBookingsAsync(cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult?.Value.Should().BeOfType<List<ReadBookingDTO>>();
        }

        [Fact]
        public async Task GetBookingAsync_ReturnsReadBookingDTO()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();

            bool isExisting = _dbContext.Restaurants.Any(currentRestaurant =>
                                    currentRestaurant.Id == restaurant.Id);

            if (!isExisting)
            {
                _dbContext.Restaurants.Add(restaurant);
            }

            Client client = ClientDataFaker.GetFakedClient();

            isExisting = _dbContext.Clients.Any(currentClient =>
                                    currentClient.Id == client.Id);

            if (!isExisting)
            {
                _dbContext.Clients.Add(client);
            }

            _dbContext.SaveChanges();

            Booking booking = BookingDataFaker.GetFakedBookingForInsert(
                restaurant.Id, client.Id);

            isExisting = _dbContext.Bookings.Any(currentBooking =>
                                    currentBooking.Id == booking.Id);

            isExisting = _dbContext.Bookings.Any(currentBooking =>
                                    currentBooking.Id == booking.Id);

            if (!isExisting)
            {
                _dbContext.Bookings.Add(booking);
                _dbContext.SaveChanges();
                _dbContext.ChangeTracker.Clear();
            }

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _bookingController
                .GetBookingAsync(booking.Id, cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult?.Value.Should().BeEquivalentTo(booking, options =>
                options.ExcludingNestedObjects().ExcludingMissingMembers()
                .Excluding(booking => booking.BookingTime));
        }

        [Fact]
        public async Task InsertBookingAsync_ReturnsReadBookingDTO()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();

            bool isExisting = _dbContext.Restaurants.Any(currentRestaurant =>
                                    currentRestaurant.Id == restaurant.Id);

            if (!isExisting)
            {
                _dbContext.Restaurants.Add(restaurant);
            }

            Client client = ClientDataFaker.GetFakedClient();

            isExisting = _dbContext.Clients.Any(currentClient =>
                                    currentClient.Id == client.Id);

            if (!isExisting)
            {
                _dbContext.Clients.Add(client);
            }

            _dbContext.SaveChanges();

            Booking booking = BookingDataFaker.GetFakedBookingForInsert(
                restaurant.Id, client.Id);

            _dbContext.Tables.Add(booking.Table);
            _dbContext.SaveChanges();

            booking.TableId = booking.Table.Id;

            var insertBookingDTO = _mapper.Map<InsertBookingDTO>(booking);

            _bookingHubServiceMock
                .MockSendBookingMessageAsync<ReadBookingDTO>(HubMessageType.Insert);

            _tokenParserMock.MockParseSubjectId(booking.ClientId);

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _bookingController
                .InsertBookingAsync(insertBookingDTO, cancellationToken);

            var createdAtActionResult = result as CreatedAtActionResult;

            //Assert
            createdAtActionResult.Should().NotBeNull();

            createdAtActionResult.Should().BeEquivalentTo(booking, options =>
                options.ExcludingNestedObjects().ExcludingMissingMembers());

            _tokenParserMock.Verify();
            _bookingHubServiceMock.Verify();
        }

        [Fact]
        public async Task DeleteBookingAsync_ReturnsNoContentResult()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();

            bool isExisting = _dbContext.Restaurants.Any(currentRestaurant =>
                                    currentRestaurant.Id == restaurant.Id);

            if (!isExisting)
            {
                _dbContext.Restaurants.Add(restaurant);
            }

            Client client = ClientDataFaker.GetFakedClient();

            isExisting = _dbContext.Clients.Any(currentClient =>
                                    currentClient.Id == client.Id);

            if (!isExisting)
            {
                _dbContext.Clients.Add(client);
            }

            _dbContext.SaveChanges();

            Booking booking = BookingDataFaker.GetFakedBookingForInsert(
                restaurant.Id, client.Id);

            isExisting = _dbContext.Bookings.Any(currentBooking =>
                                    currentBooking.Id == booking.Id);

            if (!isExisting)
            {
                _dbContext.Bookings.Add(booking);
                _dbContext.SaveChanges();
            }

            _bookingHubServiceMock
                .MockSendBookingMessageAsync(HubMessageType.Delete, booking.Id);

            _tokenParserMock.MockParseSubjectId(booking.ClientId);

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _bookingController
                .DeleteBookingAsync(booking.Id, cancellationToken);

            var noContentResult = result as NoContentResult;

            //Assert
            noContentResult.Should().NotBeNull();

            _tokenParserMock.Verify();
            _bookingHubServiceMock.Verify();
        }

        private int GetFakeTableId()
        {
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();

            bool isExisting = _dbContext.Restaurants.Any(currentRestaurant =>
                                    currentRestaurant.Id == restaurant.Id);

            if (!isExisting)
            {
                _dbContext.Restaurants.Add(restaurant);
                _dbContext.SaveChanges();
            }

            Table table = TableDataFaker.GetFakedTableForInsert(restaurant.Id);

            _dbContext.Tables.Add(table);
            _dbContext.SaveChanges();

            _dbContext.ChangeTracker.Clear();

            return table.Id;
        }
    }
}
