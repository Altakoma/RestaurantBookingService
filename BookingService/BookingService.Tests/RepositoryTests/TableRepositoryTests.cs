using AutoMapper;
using BookingService.Application.DTOs.Table;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.MappingProfiles;
using BookingService.Domain.Entities;
using BookingService.Infrastructure.Data.Repositories;
using BookingService.Tests.Fakers;
using BookingService.Tests.RepositoryTests.Base;

namespace BookingService.Tests.RepositoryTests
{
    public class TableRepositoryTests : BaseRepositoryTests<Table>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public TableRepositoryTests() : base()
        {
            _tableRepository = new TableRepository(_dbContextMock.Object,
                _mapperMock.Object);

            _mapper = new Mapper(new MapperConfiguration(
                configure => configure.AddProfile(new TableProfile())));

            _repository = _tableRepository;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsReadTableDTOs()
        {
            //Arrange
            var tables = new List<Table> { TableDataFaker.GetFakedTable() };
            IQueryable<Table> tableQuery = tables.AsQueryable();

            var tableReadDTOs = _mapper.Map<List<ReadTableDTO>>(tables);

            //Act
            //Assert
            await GetAllAsync_ReturnsEntities(tableQuery, tableReadDTOs);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsTables()
        {
            //Arrange
            var tables = new List<Table> { TableDataFaker.GetFakedTable() };
            IQueryable<Table> tableQuery = tables.AsQueryable();

            //Act
            //Assert
            await GetAllAsync_ReturnsEntities(tableQuery, tables);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsReadTableDTO()
        {
            //Arrange
            var table = TableDataFaker.GetFakedTable();
            IQueryable<Table> tableQuery = new List<Table> { table }.AsQueryable();

            var readTableDTO = _mapper.Map<ReadTableDTO>(table);

            //Act
            //Assert
            await GetByIdAsync_ReturnsEntity(table.Id, tableQuery, readTableDTO);
        }

        [Fact]
        public async Task GetByIdAsync_WhenItemIsNotExisting_ReturnsReadTableDTO()
        {
            //Arrange
            var table = TableDataFaker.GetFakedTable();

            //Act
            //Assert
            await GetByIdAsync_WhenItemIsNotExisting_ReturnsEntity<ReadTableDTO>(table.Id);
        }

        [Fact]
        public async Task InsertTableAsync_ReturnsReadTableDTO()
        {
            //Arrange
            var table = TableDataFaker.GetFakedTable();
            var readTableDTO = _mapper.Map<ReadTableDTO>(table);

            //Act
            //Assert
            await InsertAsync_ReturnsEntity(table, readTableDTO);
        }

        [Fact]
        public async Task InsertTableAsync_WhenItIsNotFound_ThrowsNotFoundException()
        {
            //Arrange
            var table = TableDataFaker.GetFakedTable();

            //Act
            //Assert
            await InsertAsync_WhenItIsNotFound_ThrowsNotFoundException<ReadTableDTO>(table);
        }

        [Fact]
        public async Task UpdateTable__ReturnsReadTableDTO()
        {
            //Arrange
            var table = TableDataFaker.GetFakedTable();
            var readTableDTO = _mapper.Map<ReadTableDTO>(table);

            //Act
            //Assert
            await UpdateEntity_ReturnsEntity(table, readTableDTO);
        }

        [Fact]
        public async Task UpdateTable__WhenItIsNotFound_ThrowsNotFoundException()
        {
            //Arrange
            var table = TableDataFaker.GetFakedTable();

            //Act
            //Assert
            await UpdateEntity__WhenItIsNotFound_ThrowsNotFoundException<ReadTableDTO>(table);
        }

        [Fact]
        public async Task DeleteBooking_WhenEntityIsExisting_SuccessfullyExecuted()
        {
            //Arrange
            var table = TableDataFaker.GetFakedTable();

            var tableQuery = new List<Table> { table }.AsQueryable();

            //Act
            //Assert
            await DeleteEntityAsync_WhenEntityIsExisting_SuccessfullyExecuted(
                tableQuery, table.Id);
        }

        [Fact]
        public async Task DeleteBookingAsync_WhenBookingIsNotExisting_ThrowsNotFoundException()
        {
            //Arrange
            var table = TableDataFaker.GetFakedTable();

            var tableQuery = new List<Table>().AsQueryable();

            //Act
            //Assert
            await DeleteEntityAsync_WhenEntityIsNotExisting_ThrowsNotFoundException(
                tableQuery, table.Id);
        }
    }
}
