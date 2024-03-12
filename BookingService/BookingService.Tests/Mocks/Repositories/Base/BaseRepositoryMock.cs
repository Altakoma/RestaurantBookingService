using BookingService.Application.Interfaces.Repositories.Base;
using Moq;

namespace BookingService.Tests.Mocks.Repositories.Base
{
    public class BaseRepositoryMock<TBaseRepository, TEntity> : Mock<TBaseRepository>
        where TBaseRepository : class, IRepository<TEntity>
    {
        public BaseRepositoryMock<TBaseRepository, TEntity> MockGetByIdAsync<TReadEntity>(
            int id, TReadEntity readEntity)
        {
            Setup(repository => repository.GetByIdAsync<TReadEntity>(
                id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(readEntity)
            .Verifiable();

            return this;
        }

        public BaseRepositoryMock<TBaseRepository, TEntity> MockGetAllAsync<TReadEntity>(
            ICollection<TReadEntity> readEntities)
        {
            Setup(repository => repository.GetAllAsync<TReadEntity>(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(readEntities)
            .Verifiable();

            return this;
        }

        public BaseRepositoryMock<TBaseRepository, TEntity> MockInsertAsync<TReadEntity>(
            TEntity insertEntity, TReadEntity readEntity)
        {
            Setup(repository => repository.InsertAsync<TReadEntity>(insertEntity,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(readEntity)
            .Verifiable();

            return this;
        }

        public BaseRepositoryMock<TBaseRepository, TEntity> MockSaveChangesAsync(bool isSaved)
        {
            Setup(repository => repository.SaveChangesToDbAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(isSaved)
            .Verifiable();

            return this;
        }

        public BaseRepositoryMock<TBaseRepository, TEntity> MockUpdateAsync<TReadEntity>(
            TEntity entity, TReadEntity readEntity)
        {
            Setup(repository => repository.UpdateAsync<TReadEntity>(entity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(readEntity)
            .Verifiable();

            return this;
        }

        public BaseRepositoryMock<TBaseRepository, TEntity> MockDeleteAsync(
            int id)
        {
            Setup(repository => repository.DeleteAsync(id,
                It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }
    }
}
