using CatalogService.Application.Interfaces.Repositories.Base;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Tests.Mocks.DbContexts;
using CatalogService.Tests.Mocks.Mappers;
using FluentAssertions;
using Moq;

namespace CatalogService.Tests.RepositoriesTests.Base
{
    public class BaseRepositoryTests<T> where T : BaseEntity
    {
        protected readonly CatalogServiceDbContextMock _catalogServiceDbContextMock;
        protected readonly MapperMock _mapperMock;

        protected IRepository<T> _repository;

        public BaseRepositoryTests()
        {
            _catalogServiceDbContextMock = new();

            _mapperMock = new();

            _repository = default!;
        }

        public async Task GetAllAsync_ReturnsEntities<U>(IQueryable<T> query,
            ICollection<U> resultCollection)
        {
            //Arrange
            _catalogServiceDbContextMock.MockDataSet(query);

            _mapperMock.MockProjectTo(query, resultCollection);

            //Act
            var result = await _repository.GetAllAsync<U>(It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(resultCollection);

            _catalogServiceDbContextMock.Verify();
            _mapperMock.Verify();
        }

        public async Task GetByIdAsync_ReturnsEntity<U>(int id, IQueryable<T> query,
            U resultObject)
        {
            //Arrange
            ICollection<U> resultCollection = new List<U> { resultObject };

            _catalogServiceDbContextMock.MockDataSet(query);

            _mapperMock.MockProjectTo(query, resultCollection);

            //Act
            var result = await _repository.GetByIdAsync<U>(id, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(resultObject);

            _catalogServiceDbContextMock.Verify();
            _mapperMock.Verify();
        }

        public async Task InsertAsync_SuccessfullyExecuted(T entity)
        {
            //Arrange
            _catalogServiceDbContextMock.MockInsertAsync(entity);

            //Act
            await _repository.InsertAsync(entity, It.IsAny<CancellationToken>());

            //Assert
            _catalogServiceDbContextMock.Verify(
                dbContext => dbContext.AddAsync(entity, It.IsAny<CancellationToken>()));
        }

        public void UpdateEntity_SuccessfullyExecuted(T entity)
        {
            //Arrange
            _catalogServiceDbContextMock.MockUpdate(entity);

            //Act
            _repository.Update(entity);

            //Assert
            _catalogServiceDbContextMock.Verify(dbContext => dbContext.Update(entity));
        }

        public void DeleteEntity_SuccessfullyExecuted(T entity)
        {
            //Arrange
            _catalogServiceDbContextMock.MockDelete(entity);

            //Act
            _repository.Delete(entity);

            //Assert
            _catalogServiceDbContextMock.Verify(dbContext => dbContext.Remove(entity));
        }

        public async Task DeleteEntityAsync_WhenEntityIsExisting_SuccessfullyExecuted(
            IQueryable<T> query, int id)
        {
            //Arrange
            _catalogServiceDbContextMock.MockDataSet(query);

            //Act
            await _repository.DeleteAsync(id, It.IsAny<CancellationToken>());

            //Assert
            _catalogServiceDbContextMock.Verify();
        }

        public async Task DeleteEntityAsync_WhenEntityIsNotExisting_ThrowsNotFoundException(
            IQueryable<T> query, int id)
        {
            //Arrange
            _catalogServiceDbContextMock.MockDataSet(query);

            //Act
            var result = _repository.DeleteAsync(id, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _catalogServiceDbContextMock.Verify();
        }
    }
}
