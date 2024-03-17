using CatalogService.Application.Interfaces.Repositories.Base;
using Moq;

namespace CatalogService.Tests.Mocks.Repositories.Base
{
    public class BaseRepositoryMock<TRepository, TEntity> : Mock<TRepository>
        where TRepository : class, IRepository<TEntity>
    {
        public BaseRepositoryMock<TRepository, TEntity> MockGetByIdAsync<TReadEntity>(
            int id, TReadEntity readEntity)
        {
            Setup(repository => repository.GetByIdAsync<TReadEntity>(
                id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(readEntity)
            .Verifiable();

            return this;
        }

        public BaseRepositoryMock<TRepository, TEntity> MockGetAllAsync<TReadEntity>(
            ICollection<TReadEntity> readEntities)
        {
            Setup(repository => repository.GetAllAsync<TReadEntity>(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(readEntities)
            .Verifiable();

            return this;
        }

        public BaseRepositoryMock<TRepository, TEntity> MockInsertAsync(TEntity insertEntity)
        {
            Setup(repository => repository.InsertAsync(insertEntity,
                It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }

        public BaseRepositoryMock<TRepository, TEntity> MockSaveChangesAsync(bool isSaved)
        {
            Setup(repository => repository.SaveChangesToDbAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(isSaved)
            .Verifiable();

            return this;
        }

        public BaseRepositoryMock<TRepository, TEntity> MockUpdate(TEntity entity)
        {
            Setup(repository => repository.Update(entity))
            .Verifiable();

            return this;
        }

        public BaseRepositoryMock<TRepository, TEntity> MockDeleteAsync(
            int id)
        {
            Setup(repository => repository.DeleteAsync(id,
                It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }
    }
}
