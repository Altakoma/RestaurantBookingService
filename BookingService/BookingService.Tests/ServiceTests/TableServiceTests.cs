using AutoMapper;
using BookingService.Application;
using BookingService.Application.DTOs.Table;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Interfaces.Services;
using BookingService.Application.MappingProfiles;
using BookingService.Application.Services;
using BookingService.Domain.Entities;
using BookingService.Domain.Exceptions;
using BookingService.Tests.Fakers;
using BookingService.Tests.Mocks.Grpc;
using BookingService.Tests.Mocks.HttpContextAccessors;
using BookingService.Tests.Mocks.Repositories;
using BookingService.Tests.Mocks.TokenParsers;
using BookingService.Tests.ServiceTests.Base;
using FluentAssertions;
using Moq;

namespace BookingService.Tests.ServiceTests
{
    public class TableServiceTests : BaseServiceTests<ITableRepository, Table>
    {
        private readonly TableRepositoryMock _tableRepositoryMock;
        private readonly HttpContextAccessorMock _httpContextAccessorMock;
        private readonly TokenParserMock _tokenParserMock;
        private readonly GrpcClientEmployeeServiceMock _grpcClient;

        private readonly ITableService _tableService;
        private readonly IMapper _mapper;

        public TableServiceTests() : base()
        {
            _tableRepositoryMock = new();
            _httpContextAccessorMock = new();
            _tokenParserMock = new();
            _grpcClient = new();

            _mapper = new Mapper(new MapperConfiguration(configure =>
            configure.AddProfile(new TableProfile())));

            _tableService = new Application.Services.TableService(_tableRepositoryMock.Object,
                _mapperMock.Object, _tokenParserMock.Object,
                _httpContextAccessorMock.Object, _grpcClient.Object);

            _baseRepositoryMock = _tableRepositoryMock;
            _baseService = _tableService;
        }

        [Fact]
        public async Task GetTableByIdAsync_ReturnsReadTableDTO()
        {
            //Arrange
            Table table = TableDataFaker.GetFakedTable();
            var readTableDTO = _mapper.Map<ReadTableDTO>(table);

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(table.Id, readTableDTO);
        }

        [Fact]
        public async Task GetAllTablesAsync_ReturnsReadTableDTOs()
        {
            //Arrange
            Table table = TableDataFaker.GetFakedTable();
            var tables = new List<Table> { table };
            var readTableDTOs = _mapper.Map<List<ReadTableDTO>>(tables);

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(readTableDTOs);
        }

        [Fact]
        public async Task DeleteTableAsync_WhenItIsSaved()
        {
            //Arrange
            Table table = TableDataFaker.GetFakedTable();
            Client client = ClientDataFaker.GetFakedClient();

            _tableRepositoryMock.MockGetRestaurantIdByTableIdAsync(table.Id, table.RestaurantId);
            _tokenParserMock.MockParseSubjectId(client.Id);

            var request = new IsWorkingAtRestaurantRequest
            {
                EmployeeId = client.Id,
                RestaurantId = table.RestaurantId,
            };

            var reply = new IsWorkingAtRestaurantReply
            {
                IsEmployeeWorkingAtRestaurant = true,
            };

            _grpcClient.MockIsEmployeeWorkingAtRestaurant(request, reply);

            //Act
            await base.DeleteAsync_WhenItIsSaved(table);

            //Assert
            _tableRepositoryMock.Verify();
            _tokenParserMock.Verify();
            _grpcClient.Verify();
        }

        [Fact]
        public async Task DeleteTableAsync_WhenItIsNotSaved_ThrowsDbOperationException()
        {
            //Arrange
            Table table = TableDataFaker.GetFakedTable();
            Client client = ClientDataFaker.GetFakedClient();

            _tableRepositoryMock.MockGetRestaurantIdByTableIdAsync(table.Id, table.RestaurantId);
            _tokenParserMock.MockParseSubjectId(client.Id);

            var request = new IsWorkingAtRestaurantRequest
            {
                EmployeeId = client.Id,
                RestaurantId = table.RestaurantId,
            };

            var reply = new IsWorkingAtRestaurantReply
            {
                IsEmployeeWorkingAtRestaurant = true,
            };

            _grpcClient.MockIsEmployeeWorkingAtRestaurant(request, reply);

            //Act
            await base.DeleteAsync_WhenItIsNotSaved_ThrowsDbOperationException(table);

            //Assert
            _tableRepositoryMock.Verify();
            _tokenParserMock.Verify();
            _grpcClient.Verify();
        }

        [Fact]
        public async Task DeleteTableAsync_WhenInitiatorIsNotWorkingAtRestaurant_ThrowsAuthorizationException()
        {
            //Arrange
            Table table = TableDataFaker.GetFakedTable();
            Client client = ClientDataFaker.GetFakedClient();

            _tableRepositoryMock.MockGetRestaurantIdByTableIdAsync(table.Id, table.RestaurantId);
            _tokenParserMock.MockParseSubjectId(client.Id);

            var request = new IsWorkingAtRestaurantRequest
            {
                EmployeeId = client.Id,
                RestaurantId = table.RestaurantId,
            };

            var reply = new IsWorkingAtRestaurantReply
            {
                IsEmployeeWorkingAtRestaurant = false,
            };

            _grpcClient.MockIsEmployeeWorkingAtRestaurant(request, reply);

            //Act
            var result = _tableService.DeleteAsync(table.Id, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<AuthorizationException>(() => result);

            _tableRepositoryMock.Verify();
            _tokenParserMock.Verify();
            _grpcClient.Verify();
        }

        [Fact]
        public async Task InsertTableAsync_ReturnsReadTableDTO()
        {
            //Arrange
            Table table = TableDataFaker.GetFakedTable();
            var insertTableDTO = _mapper.Map<InsertTableDTO>(table);
            var readTableDTO = _mapper.Map<ReadTableDTO>(table);
            Client client = ClientDataFaker.GetFakedClient();

            _tokenParserMock.MockParseSubjectId(client.Id);

            var request = new IsWorkingAtRestaurantRequest
            {
                EmployeeId = client.Id,
                RestaurantId = table.RestaurantId,
            };

            var reply = new IsWorkingAtRestaurantReply
            {
                IsEmployeeWorkingAtRestaurant = true,
            };

            _grpcClient.MockIsEmployeeWorkingAtRestaurant(request, reply);

            _mapperMock.MockMap(insertTableDTO, table);

            _baseRepositoryMock.MockInsertAsync(table, readTableDTO);

            //Act
            var result = await _tableService.InsertAsync<ReadTableDTO>(insertTableDTO,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readTableDTO);

            _tableRepositoryMock.Verify();
            _tokenParserMock.Verify();
            _grpcClient.Verify();
            _mapperMock.Verify();
            _baseRepositoryMock.Verify();
        }

        [Fact]
        public async Task InsertTableAsync_WhenInitiatorIsNotWorkingAtRestaurant_ThrowsAuthorizationException()
        {
            //Arrange
            Table table = TableDataFaker.GetFakedTable();
            var insertTableDTO = _mapper.Map<InsertTableDTO>(table);
            var readTableDTO = _mapper.Map<ReadTableDTO>(table);
            Client client = ClientDataFaker.GetFakedClient();

            _tokenParserMock.MockParseSubjectId(client.Id);

            var request = new IsWorkingAtRestaurantRequest
            {
                EmployeeId = client.Id,
                RestaurantId = table.RestaurantId,
            };

            var reply = new IsWorkingAtRestaurantReply
            {
                IsEmployeeWorkingAtRestaurant = false,
            };

            _grpcClient.MockIsEmployeeWorkingAtRestaurant(request, reply);

            //Act
            var result = _tableService.InsertAsync<ReadTableDTO>(insertTableDTO,
                It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<AuthorizationException>(() => result);

            _tableRepositoryMock.Verify();
            _tokenParserMock.Verify();
            _grpcClient.Verify();
            _mapperMock.Verify();
        }

        [Fact]
        public async Task UpdateTableAsync_ReturnsReadTableDTO()
        {
            //Arrange
            Table table = TableDataFaker.GetFakedTable();
            var updateTableDTO = _mapper.Map<UpdateTableDTO>(table);
            var readTableDTO = _mapper.Map<ReadTableDTO>(table);
            Client client = ClientDataFaker.GetFakedClient();

            _tableRepositoryMock.MockGetRestaurantIdByTableIdAsync(table.Id, table.RestaurantId);
            _tokenParserMock.MockParseSubjectId(client.Id);

            var request = new IsWorkingAtRestaurantRequest
            {
                EmployeeId = client.Id,
                RestaurantId = table.RestaurantId,
            };

            var reply = new IsWorkingAtRestaurantReply
            {
                IsEmployeeWorkingAtRestaurant = true,
            };

            _grpcClient.MockIsEmployeeWorkingAtRestaurant(request, reply);

            //Act
            await base.Update_ReturnsEntity(updateTableDTO, table, readTableDTO);

            //Assert
            _tableRepositoryMock.Verify();
            _tokenParserMock.Verify();
            _grpcClient.Verify();
        }

        [Fact]
        public async Task UpdateTableAsync_WhenInitiatorIsNotWorkingAtRestaurant_ReturnsReadTableDTO()
        {
            //Arrange
            Table table = TableDataFaker.GetFakedTable();
            var updateTableDTO = _mapper.Map<UpdateTableDTO>(table);
            var readTableDTO = _mapper.Map<ReadTableDTO>(table);
            Client client = ClientDataFaker.GetFakedClient();

            _tableRepositoryMock.MockGetRestaurantIdByTableIdAsync(table.Id, table.RestaurantId);
            _tokenParserMock.MockParseSubjectId(client.Id);

            var request = new IsWorkingAtRestaurantRequest
            {
                EmployeeId = client.Id,
                RestaurantId = table.RestaurantId,
            };

            var reply = new IsWorkingAtRestaurantReply
            {
                IsEmployeeWorkingAtRestaurant = false,
            };

            _grpcClient.MockIsEmployeeWorkingAtRestaurant(request, reply);

            //Act
            var result = _tableService.UpdateAsync<UpdateTableDTO,ReadTableDTO>(
                table.Id, updateTableDTO, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<AuthorizationException>(() => result);

            _tableRepositoryMock.Verify();
            _tokenParserMock.Verify();
            _grpcClient.Verify();
        }
    }
}
