using Moq;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Domain.Entities;

namespace OrderService.Tests.Mocks.Repositoriies
{
    public class SqlOrderRepositoryMock : Mock<ISqlOrderRepository>
    {
        public SqlOrderRepositoryMock MockGetByIdAsync<TReadDTO>(
            int id, TReadDTO readDTO)
        {
            Setup(repository => repository.GetByIdAsync<TReadDTO>(
                id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(readDTO)
            .Verifiable();

            return this;
        }

        public SqlOrderRepositoryMock MockInsertAsync<TReadDTO>(
            Order order, TReadDTO readDTO)
        {
            Setup(repository => repository.InsertAsync<TReadDTO>(order,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(readDTO)
            .Verifiable();

            return this;
        }

        public SqlOrderRepositoryMock MockSaveChangesAsync(bool isSaved)
        {
            Setup(repository => repository.SaveChangesToDbAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(isSaved)
            .Verifiable();

            return this;
        }

        public SqlOrderRepositoryMock MockUpdateAsync<TReadDTO>(
            Order order, TReadDTO readDTO)
        {
            Setup(repository => repository.UpdateAsync<TReadDTO>(order,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(readDTO)
            .Verifiable();

            return this;
        }

        public SqlOrderRepositoryMock MockDeleteAsync(
            int id)
        {
            Setup(repository => repository.DeleteAsync(id,
                It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }
    }
}
