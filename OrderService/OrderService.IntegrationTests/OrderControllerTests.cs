using FluentAssertions;
using Hangfire.Common;
using Hangfire.States;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OrderService.Application;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Repositories.NoSql;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data.ApplicationSQLDbContext;
using OrderService.Presentation.Controllers;
using OrderService.Tests.Fakers;
using OrderService.Tests.Mocks.BackgroundJobClients;
using OrderService.Tests.Mocks.GrpcClientServices;
using OrderService.Tests.Mocks.TokenParsers;

namespace OrderService.IntegrationTests
{
    public class OrderControllerTests : IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly OrderServiceSqlDbContext _sqlDbContext;
        private readonly INoSqlOrderRepository _noSqlOrderRepository;

        private readonly OrderController _orderController;

        private readonly BackgroundJobClientMock _backgroundJobClientMock;
        private readonly TokenParserMock _tokenParserMock;
        private readonly GrpcClientBookingServiceMock _grpcClientBookingServiceMock;

        private readonly IServiceScope _serviceScope;

        private readonly IMediator _mediator;

        private readonly CancellationTokenSource _cancellationTokenSource;

        public OrderControllerTests(IntegrationTestWebAppFactory factory)
        {
            _serviceScope = factory.Services.CreateScope();

            _sqlDbContext = _serviceScope.ServiceProvider
                .GetRequiredService<OrderServiceSqlDbContext>();

            _noSqlOrderRepository = _serviceScope.ServiceProvider
                .GetRequiredService<INoSqlOrderRepository>();

            _orderController = _serviceScope.ServiceProvider
                .GetRequiredService<OrderController>();

            _mediator = _serviceScope.ServiceProvider
                .GetRequiredService<IMediator>();

            _backgroundJobClientMock = factory.BackgroundJobClientMock;
            _tokenParserMock = factory.TokenParserMock;
            _grpcClientBookingServiceMock = factory.GrpcClientBookingServiceMock;

            _cancellationTokenSource = new CancellationTokenSource();
        }

        [Fact]
        public async Task GetOrderAsync_ReturnsReadOrderDTOs()
        {
            //Arrange
            ReadOrderDTO readOrderDTO = OrderDataFaker.GetFakedReadOrderDTO();

            var cancellationToken = _cancellationTokenSource.Token;

            await _noSqlOrderRepository.InsertAsync(readOrderDTO, cancellationToken);

            //Act
            var result = await _orderController
                .GetOrderAsync(readOrderDTO.Id, cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult?.Value.Should().BeEquivalentTo(readOrderDTO);
        }

        [Theory]
        [InlineData(0, 0)]
        public async Task GetAllOrderAsync_ReturnsReadOrderDTOs(int skipCount,
            int selectionAmount)
        {
            //Arrange
            ReadOrderDTO readOrderDTO = OrderDataFaker.GetFakedReadOrderDTO();

            var cancellationToken = _cancellationTokenSource.Token;

            await _noSqlOrderRepository.InsertAsync(readOrderDTO, cancellationToken);

            //Act
            var result = await _orderController
                .GetAllOrderAsync(skipCount, selectionAmount, cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult?.Value.Should().BeOfType<List<ReadOrderDTO>>();
        }

        [Fact]
        public async Task InsertOrderAsync_ReturnsReadOrderDTOs()
        {
            //Arrange
            (int menuId, int clientId) = GetEntitiesIdForOrder();

            Random random = new Random();

            int bookingId = random.Next(10);

            var insertOrderDTO = new InsertOrderDTO
            {
                BookingId = bookingId,
                MenuId = menuId,
            };

            var cancellationToken = _cancellationTokenSource.Token;

            var request = new IsClientBookedTableRequest
            {
                ClientId = clientId,
                BookingId = bookingId,
            };

            var reply = new IsClientBookedTableReply
            {
                IsClientBookedTable = true,
            };

            _grpcClientBookingServiceMock.MockIsClientBookedTable(request, reply);

            _tokenParserMock.MockParseSubjectId(clientId);

            //Act
            var result = await _orderController
                .InsertOrderAsync(insertOrderDTO, cancellationToken);

            var createdAtActionResult = result as CreatedAtActionResult;

            //Assert
            createdAtActionResult.Should().NotBeNull();

            createdAtActionResult?.Value.Should().
                BeEquivalentTo(insertOrderDTO, options =>
                options.ExcludingMissingMembers().ExcludingNestedObjects());

            _grpcClientBookingServiceMock.Verify();
            _tokenParserMock.Verify();

            _backgroundJobClientMock.Verify(client => client.Create(
                It.Is<Job>(job => job.Method.Name == nameof(_noSqlOrderRepository.InsertAsync) &&
                job.Args[0].GetType() == typeof(ReadOrderDTO)),
                It.IsAny<EnqueuedState>()));
        }

        private (int menuId, int clientId) GetEntitiesIdForOrder()
        {
            Client client = ClientDataFaker.GetFakedClient();
            Menu menu = MenuDataFaker.GetFakedMenu();

            bool isMenuExisting = _sqlDbContext.Clients
                .Any(currentMenu => currentMenu.Id == menu.Id);

            if (!isMenuExisting)
            {
                _sqlDbContext.Menu.Add(menu);
            }

            bool isUserExisting = _sqlDbContext.Clients
                .Any(currentClient => currentClient.Id == client.Id);

            if (!isUserExisting)
            {
                _sqlDbContext.Clients.Add(client);
            }

            _sqlDbContext.SaveChanges();

            return (menu.Id, client.Id);
        }
    }
}
