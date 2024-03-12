using Hangfire.Common;
using Hangfire.States;
using Moq;
using OrderService.Application.DTOs.Order;
using OrderService.Application.MediatR.Order.Commands;
using OrderService.Application.MediatR.Order.Handlers;
using OrderService.Domain.Exceptions;
using OrderService.Tests.Fakers;
using OrderService.Tests.Mocks.BackgroundJobClients;
using OrderService.Tests.Mocks.HttpContextAccessors;
using OrderService.Tests.Mocks.Repositoriies;
using OrderService.Tests.Mocks.TokenParsers;

namespace OrderService.Tests.MediatRTests.Order
{
    public class DeleteOrderHandlerTests
    {
        private readonly SqlOrderRepositoryMock _sqlOrderRepositoryMock;
        private readonly NoSqlOrderRepositoryMock _noSqlOrderRepositoryMock;
        private readonly BackgroundJobClientMock _backgroundJobClientMock;
        private readonly TokenParserMock _tokenParserMock;
        private readonly HttpContextAccessorMock _httpContextAccessorMock;

        private readonly DeleteOrderHandler _handler;

        public DeleteOrderHandlerTests()
        {
            _sqlOrderRepositoryMock = new();
            _noSqlOrderRepositoryMock = new();
            _backgroundJobClientMock = new();
            _tokenParserMock = new();
            _httpContextAccessorMock = new();

            _handler = new DeleteOrderHandler(_sqlOrderRepositoryMock.Object,
                _noSqlOrderRepositoryMock.Object, _backgroundJobClientMock.Object,
                _tokenParserMock.Object, _httpContextAccessorMock.Object);
        }

        [Fact]
        public async Task DeleteOrder_WhenItIsSaved()
        {
            //Arrange
            ReadOrderDTO readOrderDTO = OrderDataFaker.GetFakedReadOrderDTO();

            var command = new DeleteOrderCommand
            {
                Id = readOrderDTO.Id
            };

            var isSaved = true;

            _sqlOrderRepositoryMock.MockDeleteAsync(readOrderDTO.Id)
                                   .MockSaveChangesAsync(isSaved);

            _noSqlOrderRepositoryMock.MockGetOrderByIdAsync(readOrderDTO.Id, readOrderDTO);

            _tokenParserMock.MockParseSubjectId(readOrderDTO.ReadClientDTO.Id);

            //Act
            await _handler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            _sqlOrderRepositoryMock.Verify();
            _noSqlOrderRepositoryMock.Verify();
            _tokenParserMock.Verify();

            _backgroundJobClientMock.Verify(client => client.Create(
                It.Is<Job>(job => job.Method.Name == nameof(_noSqlOrderRepositoryMock.Object.DeleteAsync) &&
                job.Args[0].Equals(readOrderDTO.Id)),
                It.IsAny<EnqueuedState>()));
        }

        [Fact]
        public async Task DeleteOrder_WhenItIsNotSaved_ThrowsDbOperationException()
        {
            //Arrange
            ReadOrderDTO readOrderDTO = OrderDataFaker.GetFakedReadOrderDTO();

            var command = new DeleteOrderCommand
            {
                Id = readOrderDTO.Id
            };

            var isSaved = false;

            _sqlOrderRepositoryMock.MockDeleteAsync(readOrderDTO.Id)
                                   .MockSaveChangesAsync(isSaved);

            _noSqlOrderRepositoryMock.MockGetOrderByIdAsync(readOrderDTO.Id, readOrderDTO);

            _tokenParserMock.MockParseSubjectId(readOrderDTO.ReadClientDTO.Id);

            //Act
            var result = _handler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<DbOperationException>(() => result);

            _sqlOrderRepositoryMock.Verify();
            _noSqlOrderRepositoryMock.Verify();
            _tokenParserMock.Verify();
        }

        [Fact]
        public async Task DeleteOrder_WhenInitiatorIsNotOrdersClient_ThrowsAuthorizationException()
        {
            //Arrange
            ReadOrderDTO readOrderDTO = OrderDataFaker.GetFakedReadOrderDTO();

            var command = new DeleteOrderCommand
            {
                Id = readOrderDTO.Id
            };

            _noSqlOrderRepositoryMock.MockGetOrderByIdAsync(readOrderDTO.Id, readOrderDTO);

            _tokenParserMock.MockParseSubjectId(
                It.Is<int>(number => number != readOrderDTO.ReadClientDTO.Id));

            //Act
            var result = _handler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<AuthorizationException>(() => result);

            _noSqlOrderRepositoryMock.Verify();
            _tokenParserMock.Verify();
        }

        [Fact]
        public async Task DeleteOrder_WhenItIsNotFound_ThrowsNotFoundException()
        {
            //Arrange
            ReadOrderDTO readOrderDTO = OrderDataFaker.GetFakedReadOrderDTO();

            var command = new DeleteOrderCommand
            {
                Id = readOrderDTO.Id
            };

            _noSqlOrderRepositoryMock.MockGetOrderByIdAsync(readOrderDTO.Id, default!);

            //Act
            var result = _handler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _noSqlOrderRepositoryMock.Verify();
        }
    }
}
