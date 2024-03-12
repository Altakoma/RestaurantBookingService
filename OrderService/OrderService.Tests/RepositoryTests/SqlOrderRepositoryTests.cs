using AutoMapper;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Application.MappingProfiles;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Repositories.Sql;
using OrderService.Tests.Fakers;
using OrderService.Tests.RepositoryTests.Base;

namespace OrderService.Tests.RepositoryTests
{
    public class SqlOrderRepositoryTests : BaseSqlRepositoryTests<Order>
    {
        private readonly ISqlOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public SqlOrderRepositoryTests() : base()
        {
            _orderRepository = new SqlOrderRepository(_dbContextMock.Object,
                _mapperMock.Object);

            var mappingProfiles = new List<Profile>
            {
                new OrderProfile(),
                new ClientProfile(),
                new MenuProfile(),
            };

            _mapper = new Mapper(new MapperConfiguration(
                configure => configure.AddProfiles(mappingProfiles)));

            _repository = _orderRepository;
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsReadOrderDTO()
        {
            //Arrange
            var order = OrderDataFaker.GetFakedOrder();
            IQueryable<Order> orderQuery = new List<Order> { order }.AsQueryable();

            var readOrderDTO = _mapper.Map<ReadOrderDTO>(order);

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(order.Id, orderQuery, readOrderDTO);
        }

        [Fact]
        public async Task GetByIdAsync_WhenItemIsNotExisting_ReturnsReadOrderDTO()
        {
            //Arrange
            var order = OrderDataFaker.GetFakedOrder();

            //Act
            //Assert
            await base.GetByIdAsync_WhenItemIsNotExisting_ReturnsEntity<ReadOrderDTO>(order.Id);
        }

        [Fact]
        public async Task InsertOrderAsync_ReturnsReadOrderDTO()
        {
            //Arrange
            var order = OrderDataFaker.GetFakedOrder();
            var readOrderDTO = _mapper.Map<ReadOrderDTO>(order);

            //Act
            //Assert
            await base.InsertAsync_ReturnsEntity(order, readOrderDTO);
        }

        [Fact]
        public async Task InsertOrderAsync_WhenItIsNotFound_ThrowsNotFoundException()
        {
            //Arrange
            var order = OrderDataFaker.GetFakedOrder();

            //Act
            //Assert
            await base.InsertAsync_WhenItIsNotFound_ThrowsNotFoundException<ReadOrderDTO>(order);
        }

        [Fact]
        public async Task UpdateOrder__ReturnsReadOrderDTO()
        {
            //Arrange
            var order = OrderDataFaker.GetFakedOrder();
            var readOrderDTO = _mapper.Map<ReadOrderDTO>(order);

            //Act
            //Assert
            await base.UpdateEntity_ReturnsEntity(order, readOrderDTO);
        }

        [Fact]
        public async Task UpdateOrder__WhenItIsNotFound_ThrowsNotFoundException()
        {
            //Arrange
            var order = OrderDataFaker.GetFakedOrder();

            //Act
            //Assert
            await base.UpdateEntity__WhenItIsNotFound_ThrowsNotFoundException<ReadOrderDTO>(order);
        }

        [Fact]
        public async Task DeleteOrder_WhenEntityIsExisting_SuccessfullyExecuted()
        {
            //Arrange
            var order = OrderDataFaker.GetFakedOrder();

            var orderQuery = new List<Order> { order }.AsQueryable();

            //Act
            //Assert
            await base.DeleteEntityAsync_WhenEntityIsExisting_SuccessfullyExecuted(
                orderQuery, order.Id);
        }

        [Fact]
        public async Task DeleteOrderAsync_WhenOrderIsNotExisting_ThrowsNotFoundException()
        {
            //Arrange
            var order = OrderDataFaker.GetFakedOrder();

            var orderQuery = new List<Order>().AsQueryable();

            //Act
            //Assert
            await base.DeleteEntityAsync_WhenEntityIsNotExisting_ThrowsNotFoundException(
                orderQuery, order.Id);
        }
    }
}
