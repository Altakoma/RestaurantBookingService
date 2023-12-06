using FluentAssertions;
using Moq;
using OrderService.Application.DTOs.Order;
using OrderService.Application.MediatR.Order.Handlers;
using OrderService.Application.MediatR.Order.Queries;
using OrderService.Domain.Exceptions;
using OrderService.Tests.Fakers;
using OrderService.Tests.Mocks.Repositoriies;

namespace OrderService.Tests.MediatRTests.Order
{
    public class GetOrderByIdHandlerTests
    {
        private readonly NoSqlOrderRepositoryMock _noSqlOrderRepositoryMock;
        private readonly GetOrderByIdHandler _handler;

        public GetOrderByIdHandlerTests()
        {
            _noSqlOrderRepositoryMock = new();

            _handler = new GetOrderByIdHandler(_noSqlOrderRepositoryMock.Object);
        }

        [Fact]
        public async Task GetOrderById_ReturnsReadOrderDTO()
        {
            //Arrange
            ReadOrderDTO orderDTO = OrderDataFaker.GetFakedReadOrderDTO();

            var query = new GetOrderByIdQuery
            {
                Id = orderDTO.Id,
            };

            _noSqlOrderRepositoryMock.MockGetOrderByIdAsync(query.Id, orderDTO);

            //Act
            var result = await _handler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(orderDTO);

            _noSqlOrderRepositoryMock.Verify();
        }

        [Fact]
        public async Task GetOrderById_WhenItIsNotExisting_ThrowsNotFoundException()
        {
            //Arrange
            ReadOrderDTO orderDTO = OrderDataFaker.GetFakedReadOrderDTO();

            var query = new GetOrderByIdQuery
            {
                Id = orderDTO.Id,
            };

            _noSqlOrderRepositoryMock.MockGetOrderByIdAsync(query.Id, default!);

            //Act
            var result = _handler.Handle(query, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _noSqlOrderRepositoryMock.Verify();
        }
    }
}
