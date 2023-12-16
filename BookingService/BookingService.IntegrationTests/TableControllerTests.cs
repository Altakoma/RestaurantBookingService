using BookingService.Application;
using BookingService.Application.DTOs.Table;
using BookingService.Domain.Entities;
using BookingService.Infrastructure.Data.ApplicationDbContext;
using BookingService.Presentation.Controllers;
using BookingService.Tests.Fakers;
using BookingService.Tests.Mocks.Grpc;
using BookingService.Tests.Mocks.TokenParsers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.IntegrationTests
{
    public class TableControllerTests : IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly IServiceScope _serviceScope;

        private readonly GrpcClientEmployeeServiceMock _grpcClientEmployeeServiceMock;
        private readonly TokenParserMock _tokenParserMock;

        private readonly BookingServiceDbContext _dbContext;

        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly TableController _tableController;

        public TableControllerTests(IntegrationTestWebAppFactory factory)
        {
            _serviceScope = factory.Services.CreateScope();

            _grpcClientEmployeeServiceMock = factory.GrpcClientEmployeeServiceMock;
            _tokenParserMock = factory.TokenParserMock;

            _dbContext = _serviceScope.ServiceProvider
               .GetRequiredService<BookingServiceDbContext>();

            _cancellationTokenSource = new CancellationTokenSource();

            _tableController = _serviceScope.ServiceProvider
                .GetRequiredService<TableController>();
        }

        [Fact]
        public async Task GetAllTablesAsync_ReturnsReadTableDTOs()
        {
            //Arrange
            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _tableController.GetAllTablesAsync(cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult?.Value.Should().BeOfType<List<ReadTableDTO>>();
        }

        [Fact]
        public async Task GetTableAsync_ReturnsReadTableDTO()
        {
            //Arrange
            int restaurantId = GetFakeRestaurantId();

            Table table = TableDataFaker.GetFakedTableForInsert(restaurantId);

            _dbContext.Tables.Add(table);
            _dbContext.SaveChanges();

            _dbContext.ChangeTracker.Clear();

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _tableController.GetTableAsync(table.Id, cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult?.Value.Should().BeOfType<ReadTableDTO>();
            okResult?.Value.Should().BeEquivalentTo(table, options =>
                options.ExcludingNestedObjects().ExcludingMissingMembers());
        }

        [Theory]
        [InlineData(10)]
        public async Task InsertTableAsync_ReturnsReadTableDTO(int threshold)
        {
            //Arrange
            Random random = new Random();

            int subjectId = random.Next(threshold);
            int seatsCount = random.Next(threshold);
            int restaurantId = GetFakeRestaurantId();

            var insertTableDTO = new InsertTableDTO
            {
                SeatsCount = seatsCount,
                RestaurantId = restaurantId
            };

            var request = new IsWorkingAtRestaurantRequest
            {
                EmployeeId = subjectId,
                RestaurantId = restaurantId
            };

            var reply = new IsWorkingAtRestaurantReply
            {
                IsEmployeeWorkingAtRestaurant = true,
            };

            _tokenParserMock.MockParseSubjectId(subjectId);

            _grpcClientEmployeeServiceMock
                .MockIsEmployeeWorkingAtRestaurant(request, reply);

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _tableController.InsertTableAsync(
                                insertTableDTO, cancellationToken);

            var createdAtActionResult = result as CreatedAtActionResult;

            //Assert
            createdAtActionResult.Should().NotBeNull();

            createdAtActionResult.Should().BeEquivalentTo(insertTableDTO, options =>
                options.ExcludingNestedObjects().ExcludingMissingMembers());

            _tokenParserMock.Verify();
            _grpcClientEmployeeServiceMock.Verify();
        }

        [Theory]
        [InlineData(10)]
        public async Task DeleteTableAsync_ReturnsNoContentResult(int threshold)
        {
            //Arrange
            int restaurantId = GetFakeRestaurantId();

            Table table = TableDataFaker.GetFakedTableForInsert(restaurantId);

            _dbContext.Tables.Add(table);
            _dbContext.SaveChanges();

            _dbContext.ChangeTracker.Clear();

            Random random = new Random();

            int subjectId = random.Next(threshold);

            var request = new IsWorkingAtRestaurantRequest
            {
                EmployeeId = subjectId,
                RestaurantId = restaurantId
            };

            var reply = new IsWorkingAtRestaurantReply
            {
                IsEmployeeWorkingAtRestaurant = true,
            };

            _tokenParserMock.MockParseSubjectId(subjectId);

            _grpcClientEmployeeServiceMock
                .MockIsEmployeeWorkingAtRestaurant(request, reply);

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _tableController.DeleteTableAsync(
                                table.Id, cancellationToken);

            var noContentResult = result as NoContentResult;

            Table? resultTable = _dbContext.Tables.FirstOrDefault(currentTable =>
                currentTable.Id == table.Id);

            //Assert
            noContentResult.Should().NotBeNull();
            resultTable.Should().BeNull();

            _tokenParserMock.Verify();
            _grpcClientEmployeeServiceMock.Verify();
        }

        private int GetFakeRestaurantId()
        {
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();

            bool isExisting = _dbContext.Restaurants.Any(currentRestaurant =>
                                    currentRestaurant.Id == restaurant.Id);

            if (!isExisting)
            {
                _dbContext.Restaurants.Add(restaurant);
                _dbContext.SaveChanges();
            }

            _dbContext.ChangeTracker.Clear();

            return restaurant.Id;
        }
    }
}
