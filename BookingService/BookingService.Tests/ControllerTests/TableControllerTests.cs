using AutoMapper;
using BookingService.Application.DTOs.Table;
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
    public class TableControllerTests
    {
        private readonly TableServiceMock _tableServiceMock;

        private readonly IMapper _mapper;
        private readonly TableController _tableController;

        public TableControllerTests()
        {
            _tableServiceMock = new();

            _mapper = new Mapper(new MapperConfiguration(configure =>
                configure.AddProfile(new TableProfile())));

            _tableController = new TableController(_tableServiceMock.Object);
        }

        [Fact]
        public async Task GetAllTablesAsync_ReturnsReadTableDTOs()
        {
            //Arrange
            Table table = TableDataFaker.GetFakedTable();
            var tables = new List<Table> { table };
            var readTableDTOs = _mapper.Map<List<ReadTableDTO>>(tables);

            _tableServiceMock.MockGetAllAsync(readTableDTOs);

            //Act
            var result = await _tableController.GetAllTablesAsync(It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(readTableDTOs);
        }

        [Fact]
        public async Task GetTableAsync_ReturnsReadTableDTO()
        {
            //Arrange
            Table table = TableDataFaker.GetFakedTable();
            var readTableDTO = _mapper.Map<ReadTableDTO>(table);

            _tableServiceMock.MockGetItemAsync(readTableDTO);

            //Act
            var result = await _tableController.GetTableAsync(table.Id,
                It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(readTableDTO);
        }

        [Fact]
        public async Task InsertTableAsync_ReturnsReadTableDTO()
        {
            //Arrange
            Table table = TableDataFaker.GetFakedTable();
            var insertTableDTO = _mapper.Map<InsertTableDTO>(table);
            var readTableDTO = _mapper.Map<ReadTableDTO>(table);

            _tableServiceMock.MockInsertItemAsync(insertTableDTO,
                readTableDTO);

            //Act
            var result = await _tableController.InsertTableAsync(insertTableDTO,
                It.IsAny<CancellationToken>());
            var createdAtActionResult = result as CreatedAtActionResult;

            //Assert
            result.Should().BeOfType(typeof(CreatedAtActionResult));

            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult!.Value.Should().BeEquivalentTo(readTableDTO);
        }

        [Fact]
        public async Task DeleteTableAsync_ReturnsNoContent()
        {
            //Arrange
            Table table = TableDataFaker.GetFakedTable();

            _tableServiceMock.MockDeleteItemAsync(table.Id);

            //Act
            var result = await _tableController.DeleteTableAsync(table.Id,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeOfType(typeof(NoContentResult));
        }
    }
}
