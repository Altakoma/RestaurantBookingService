using Moq;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Repositories.NoSql;
using OrderService.Domain.Entities;

namespace OrderService.Tests.Mocks.Repositoriies
{
    public class NoSqlOrderRepositoryMock : Mock<INoSqlOrderRepository>
    {
        public NoSqlOrderRepositoryMock MockGetAllOrdersAsync(int skipCount,
            int selectionAmount, ICollection<ReadOrderDTO> orders)
        {
            Setup(repository => repository.GetAllAsync(skipCount,
                selectionAmount, It.IsAny<CancellationToken>()))
            .ReturnsAsync(orders)
            .Verifiable();

            return this;
        }

        public NoSqlOrderRepositoryMock MockGetOrderByIdAsync(int id, ReadOrderDTO readOrderDTO)
        {
            Setup(repository => repository.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(readOrderDTO)
            .Verifiable();

            return this;
        }

        public NoSqlOrderRepositoryMock MockDeleteAsync(int id)
        {
            Setup(repository => repository.DeleteAsync(id, It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }
    }
}
