using AutoMapper;
using FluentAssertions;
using Hangfire.Common;
using Hangfire.States;
using Moq;
using OrderService.Application.DTOs.Order;
using OrderService.Application.MappingProfiles;
using OrderService.Application.MediatR.Order.Commands;
using OrderService.Application.MediatR.Order.Handlers;
using OrderService.Domain.Exceptions;
using OrderService.Tests.Fakers;
using OrderService.Tests.Mocks.BackgroundJobClients;
using OrderService.Tests.Mocks.HttpContextAccessors;
using OrderService.Tests.Mocks.Mappers;
using OrderService.Tests.Mocks.Repositoriies;
using OrderService.Tests.Mocks.TokenParsers;

namespace OrderService.Tests.MediatRTests.Order
{
    public class UpdateOrderHandlerTests
    {
        private readonly SqlOrderRepositoryMock _sqlOrderRepositoryMock;
        private readonly NoSqlOrderRepositoryMock _noSqlOrderRepositoryMock;
        private readonly BackgroundJobClientMock _backgroundJobClientMock;
        private readonly MapperMock _mapperMock;
        private readonly TokenParserMock _tokenParserMock;
        private readonly HttpContextAccessorMock _httpContextAccessorMock;

        private readonly IMapper _mapper;
        private readonly UpdateOrderHandler _handler;

        public UpdateOrderHandlerTests()
        {
            _sqlOrderRepositoryMock = new();
            _noSqlOrderRepositoryMock = new();
            _backgroundJobClientMock = new();
            _httpContextAccessorMock = new();
            _tokenParserMock = new();
            _mapperMock = new();

            _handler = new UpdateOrderHandler(_sqlOrderRepositoryMock.Object,
                _noSqlOrderRepositoryMock.Object, _backgroundJobClientMock.Object,
                _tokenParserMock.Object, _httpContextAccessorMock.Object,
                _mapperMock.Object);

            var profiles = new List<Profile>
            {
                new OrderProfile(),
                new ClientProfile(),
                new MenuProfile(),
            };

            _mapper = new Mapper(new MapperConfiguration(configure =>
                configure.AddProfiles(profiles)
            ));
        }

        [Fact]
        public async Task UpdateOrder_WhenItIsSaved_ReturnsReadOrderDTO()
        {
            //Arrange
            ReadOrderDTO readOrderDTO = OrderDataFaker.GetFakedReadOrderDTO();
            var order = _mapper.Map<Domain.Entities.Order>(readOrderDTO);

            var command = new UpdateOrderCommand
            {
                Id = order.Id,
                ClientId = order.ClientId,
                MenuId = order.MenuId,
            };

            _mapperMock.MockMap(readOrderDTO, order)
                       .MockRefMap(command, order);

            _sqlOrderRepositoryMock.MockUpdateAsync(order, readOrderDTO);

            _noSqlOrderRepositoryMock.MockGetOrderByIdAsync(readOrderDTO.Id, readOrderDTO);

            _tokenParserMock.MockParseSubjectId(readOrderDTO.ReadClientDTO.Id);

            //Act
            var result = await _handler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readOrderDTO);

            _mapperMock.Verify();
            _sqlOrderRepositoryMock.Verify();
            _noSqlOrderRepositoryMock.Verify();
            _tokenParserMock.Verify();

            _backgroundJobClientMock.Verify(client => client.Create(
                It.Is<Job>(job => job.Method.Name == nameof(_noSqlOrderRepositoryMock.Object.UpdateAsync) &&
                job.Args[0].Equals(readOrderDTO)),
                It.IsAny<EnqueuedState>()));
        }

        [Fact]
        public async Task UpdateOrder_WhenInitiatorIsNotOrdersClient_ThrowsAuthorizationException()
        {
            //Arrange
            ReadOrderDTO readOrderDTO = OrderDataFaker.GetFakedReadOrderDTO();

            var command = new UpdateOrderCommand
            {
                Id = readOrderDTO.Id,
                ClientId = readOrderDTO.ReadClientDTO.Id,
                MenuId = readOrderDTO.ReadMenuDTO.Id,
            };

            _noSqlOrderRepositoryMock.MockGetOrderByIdAsync(readOrderDTO.Id, readOrderDTO);

            int subjectId = -1;

            _tokenParserMock.MockParseSubjectId(subjectId);

            //Act
            var result = _handler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<AuthorizationException>(() => result);

            _noSqlOrderRepositoryMock.Verify();
            _tokenParserMock.Verify();
        }

        [Fact]
        public async Task UpdateOrder_WhenItIsNotFound_ThrowsNotFoundException()
        {
            //Arrange
            ReadOrderDTO readOrderDTO = OrderDataFaker.GetFakedReadOrderDTO();

            var command = new UpdateOrderCommand
            {
                Id = readOrderDTO.Id,
                ClientId = readOrderDTO.ReadClientDTO.Id,
                MenuId = readOrderDTO.ReadMenuDTO.Id,
            };

            _noSqlOrderRepositoryMock.MockGetOrderByIdAsync(readOrderDTO.Id, default!);

            //Act
            var result = _handler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _noSqlOrderRepositoryMock.Verify();
            _tokenParserMock.Verify();
        }
    }
}
