using AutoMapper;
using FluentAssertions;
using Hangfire.Common;
using Hangfire.States;
using Moq;
using OrderService.Application;
using OrderService.Application.DTOs.Order;
using OrderService.Application.MappingProfiles;
using OrderService.Application.MediatR.Order.Commands;
using OrderService.Application.MediatR.Order.Handlers;
using OrderService.Domain.Exceptions;
using OrderService.Tests.Fakers;
using OrderService.Tests.Mocks.BackgroundJobClients;
using OrderService.Tests.Mocks.GrpcClientServices;
using OrderService.Tests.Mocks.HttpContextAccessors;
using OrderService.Tests.Mocks.Mappers;
using OrderService.Tests.Mocks.Repositoriies;
using OrderService.Tests.Mocks.TokenParsers;

namespace OrderService.Tests.MediatRTests.Order
{
    public class InsertOrderHandlerTests
    {
        private readonly SqlOrderRepositoryMock _sqlOrderRepositoryMock;
        private readonly NoSqlOrderRepositoryMock _noSqlOrderRepositoryMock;
        private readonly BackgroundJobClientMock _backgroundJobClientMock;
        private readonly MapperMock _mapperMock;
        private readonly TokenParserMock _tokenParserMock;
        private readonly HttpContextAccessorMock _httpContextAccessorMock;
        private readonly GrpcClientBookingServiceMock _grpcClientBookingServiceMock;

        private readonly IMapper _mapper;
        private readonly InsertOrderHandler _handler;

        public InsertOrderHandlerTests()
        {
            _sqlOrderRepositoryMock = new();
            _noSqlOrderRepositoryMock = new();
            _backgroundJobClientMock = new();
            _httpContextAccessorMock = new();
            _tokenParserMock = new();
            _mapperMock = new();
            _grpcClientBookingServiceMock = new();

            _handler = new InsertOrderHandler(_sqlOrderRepositoryMock.Object,
                _noSqlOrderRepositoryMock.Object, _backgroundJobClientMock.Object,
                _grpcClientBookingServiceMock.Object, _tokenParserMock.Object,
                _httpContextAccessorMock.Object, _mapperMock.Object);

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
        public async Task InsertOrder_WhenItIsSaved_ReturnsReadOrderDTO()
        {
            //Arrange
            ReadOrderDTO readOrderDTO = OrderDataFaker.GetFakedReadOrderDTO();
            var order = _mapper.Map<Domain.Entities.Order>(readOrderDTO);

            var command = new InsertOrderCommand
            {
                BookingId = order.BookingId,
                ClientId = order.ClientId,
                MenuId = order.MenuId,
            };

            var isClientBookedTable = new IsClientBookedTableRequest
            {
                BookingId = order.BookingId,
                ClientId = order.ClientId,
            };

            var isClientBookedTableReply = new IsClientBookedTableReply
            {
                IsClientBookedTable = true
            };

            _mapperMock.MockMap(command, order);

            _grpcClientBookingServiceMock.MockIsClientBookedTable(isClientBookedTable,
                isClientBookedTableReply);

            _sqlOrderRepositoryMock.MockInsertAsync(order, readOrderDTO);

            _tokenParserMock.MockParseSubjectId(readOrderDTO.ReadClientDTO.Id);

            //Act
            var result = await _handler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readOrderDTO);

            _mapperMock.Verify();
            _grpcClientBookingServiceMock.Verify();
            _sqlOrderRepositoryMock.Verify();
            _noSqlOrderRepositoryMock.Verify();
            _tokenParserMock.Verify();

            _backgroundJobClientMock.Verify(client => client.Create(
                It.Is<Job>(job => job.Method.Name == nameof(_noSqlOrderRepositoryMock.Object.InsertAsync) &&
                job.Args[0].Equals(readOrderDTO)),
                It.IsAny<EnqueuedState>()));
        }

        [Fact]
        public async Task InsertOrder_WhenInitiatorIsNotBookedTable_ThrowsAuthorizationException()
        {
            //Arrange
            ReadOrderDTO readOrderDTO = OrderDataFaker.GetFakedReadOrderDTO();
            var order = _mapper.Map<Domain.Entities.Order>(readOrderDTO);

            var command = new InsertOrderCommand
            {
                BookingId = order.BookingId,
                ClientId = order.ClientId,
                MenuId = order.MenuId,
            };

            var isClientBookedTable = new IsClientBookedTableRequest
            {
                BookingId = order.BookingId,
                ClientId = order.ClientId,
            };

            var isClientBookedTableReply = new IsClientBookedTableReply
            {
                IsClientBookedTable = false
            };

            _grpcClientBookingServiceMock.MockIsClientBookedTable(isClientBookedTable,
                isClientBookedTableReply);

            _tokenParserMock.MockParseSubjectId(readOrderDTO.ReadClientDTO.Id);

            //Act
            var result = _handler.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<AuthorizationException>(() => result);

            _grpcClientBookingServiceMock.Verify();
            _tokenParserMock.Verify();
        }
    }
}
