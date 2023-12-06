using FluentAssertions;
using Moq;
using OrderService.Application.DTOs.Order;
using OrderService.Application.MediatR.Order.Handlers;
using OrderService.Application.MediatR.Order.Queries;
using OrderService.Tests.Fakers;
using OrderService.Tests.Mocks.Repositoriies;

namespace OrderService.Tests.MediatRTests.Order
{
    public class GetAllOrdersHandlerTests
    {
        private readonly NoSqlOrderRepositoryMock _noSqlOrderRepositoryMock;
        private readonly GetAllOrdersHandler _handler;

        public GetAllOrdersHandlerTests()
        {
            _noSqlOrderRepositoryMock = new();

            _handler = new GetAllOrdersHandler(_noSqlOrderRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllOrders_ReturnsReadOrderDTOs()
        {
            //Arrange
            var orderDTOs = new List<ReadOrderDTO> { OrderDataFaker.GetFakedReadOrderDTO() };

            Random random = new Random();

            int skipCount = random.Next(maxValue: 10);
            int selectionAmount = random.Next(maxValue: 10);

            var query = new GetAllOrdersQuery
            {
                SelectionAmount = selectionAmount,
                SkipCount = skipCount
            };

            _noSqlOrderRepositoryMock.MockGetAllOrdersAsync(skipCount, selectionAmount, orderDTOs);

            //Act
            var result = await _handler.Handle(query,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(orderDTOs);

            _noSqlOrderRepositoryMock.Verify();
        }
    }
}
