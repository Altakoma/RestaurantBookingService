using BookingService.Application.Interfaces.Repositories.Base;
using BookingService.Domain.Entities;
using BookingService.Domain.Exceptions;
using BookingService.Tests.Mocks.DbContexts;
using BookingService.Tests.Mocks.Mappers;
using FluentAssertions;
using Moq;

namespace BookingService.Tests.RepositoryTests.Base
{
    public class BaseRepositoryTests<T> where T : BaseEntity
    {
        protected readonly BookingServiceDbContextMock _dbContextMock;
        protected readonly MapperMock _mapperMock;

        protected IRepository<T> _repository;

        public BaseRepositoryTests()
        {
            _dbContextMock = new();

            _mapperMock = new();

            _repository = default!;
        }

        public async Task GetAllAsync_ReturnsEntities<U>(IQueryable<T> query,
            ICollection<U> resultCollection)
        {
            //Arrange
            _dbContextMock.MockDataSet(query);

            _mapperMock.MockProjectTo(query, resultCollection);

            //Act
            var result = await _repository.GetAllAsync<U>(It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(resultCollection);

            _dbContextMock.Verify();
            _mapperMock.Verify();
        }

        public async Task GetByIdAsync_ReturnsEntity<U>(int id, IQueryable<T> query,
            U resultObject)
        {
            //Arrange
            ICollection<U> resultCollection = new List<U> { resultObject };

            _dbContextMock.MockDataSet(query);

            _mapperMock.MockProjectTo(query, resultCollection);

            //Act
            var result = await _repository.GetByIdAsync<U>(id, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(resultObject);

            _dbContextMock.Verify();
            _mapperMock.Verify();
        }

        public async Task GetByIdAsync_WhenItemIsNotExisting_ReturnsEntity<U>(int id)
        {
            //Arrange
            var query = new List<T>().AsQueryable();
            var resultCollection = new List<U>();

            _dbContextMock.MockDataSet(query);

            _mapperMock.MockProjectTo(query, resultCollection);

            //Act
            var result = _repository.GetByIdAsync<U>(id, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _dbContextMock.Verify();
            _mapperMock.Verify();
        }

        public async Task InsertAsync_ReturnsEntity<U>(T entity,
            U readEntity)
        {
            //Arrange
            _dbContextMock.MockInsertAsync(entity);
            _dbContextMock.MockSaveChangesToDbAsync();

            var entityCollection = new List<T> { entity };
            var resultCollection = new List<U> { readEntity };
            _dbContextMock.MockDataSet(entityCollection.AsQueryable());

            _mapperMock.MockProjectTo(entityCollection.AsQueryable(), resultCollection);

            //Act
            var result = await _repository.InsertAsync<U>(entity, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readEntity);

            _dbContextMock.Verify(
                dbContext => dbContext.AddAsync(entity, It.IsAny<CancellationToken>()));
            _dbContextMock.Verify(
                dbContext => dbContext.SaveChangesAsync(It.IsAny<CancellationToken>()));
            _dbContextMock.Verify(
                dbContext => dbContext.Set<T>());
            _mapperMock.Verify();
        }

        public async Task InsertAsync_WhenItIsNotFound_ThrowsNotFoundException<U>(T entity)
        {
            //Arrange
            _dbContextMock.MockInsertAsync(entity);
            _dbContextMock.MockSaveChangesToDbAsync();

            var entityQuery = new List<T> { entity }.AsQueryable();
            var resultCollection = new List<U>();

            _dbContextMock.MockDataSet(entityQuery);

            _mapperMock.MockProjectTo(entityQuery, resultCollection);

            //Act
            var result = _repository.InsertAsync<U>(entity, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _dbContextMock.Verify(
                dbContext => dbContext.AddAsync(entity, It.IsAny<CancellationToken>()));
            _dbContextMock.Verify(
                dbContext => dbContext.SaveChangesAsync(It.IsAny<CancellationToken>()));
            _dbContextMock.Verify(
                dbContext => dbContext.Set<T>());
            _mapperMock.Verify();
        }

        public async Task UpdateEntity_ReturnsEntity<U>(T entity, U readEntity)
        {
            //Arrange
            _dbContextMock.MockUpdate(entity);
            _dbContextMock.MockSaveChangesToDbAsync();

            var entityCollection = new List<T> { entity };
            var resultCollection = new List<U> { readEntity };
            _dbContextMock.MockDataSet(entityCollection.AsQueryable());

            _mapperMock.MockProjectTo(entityCollection.AsQueryable(), resultCollection);

            //Act
            var result = await _repository.UpdateAsync<U>(
                entity, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readEntity);

            _dbContextMock.Verify(dbContext => dbContext.Update(entity));
            _dbContextMock.Verify(
                dbContext => dbContext.SaveChangesAsync(It.IsAny<CancellationToken>()));
            _dbContextMock.Verify(
                dbContext => dbContext.Set<T>());
            _mapperMock.Verify();
        }

        public async Task UpdateEntity__WhenItIsNotFound_ThrowsNotFoundException<U>(T entity)
        {
            //Arrange
            _dbContextMock.MockUpdate(entity);
            _dbContextMock.MockSaveChangesToDbAsync();

            var entityQuery = new List<T> { entity }.AsQueryable();
            var resultCollection = new List<U>();

            _dbContextMock.MockDataSet(entityQuery);

            _mapperMock.MockProjectTo(entityQuery, resultCollection);

            //Act
            var result = _repository.UpdateAsync<U>(
                entity, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _dbContextMock.Verify(dbContext => dbContext.Update(entity));
            _dbContextMock.Verify(
                dbContext => dbContext.SaveChangesAsync(It.IsAny<CancellationToken>()));
            _dbContextMock.Verify(
                dbContext => dbContext.Set<T>());
            _mapperMock.Verify();
        }

        public async Task DeleteEntityAsync_WhenEntityIsExisting_SuccessfullyExecuted(
            IQueryable<T> query, int id)
        {
            //Arrange
            _dbContextMock.MockDataSet(query);

            //Act
            await _repository.DeleteAsync(id, It.IsAny<CancellationToken>());

            //Assert
            _dbContextMock.Verify();
        }

        public async Task DeleteEntityAsync_WhenEntityIsNotExisting_ThrowsNotFoundException(
            IQueryable<T> query, int id)
        {
            //Arrange
            _dbContextMock.MockDataSet(query);

            //Act
            var result = _repository.DeleteAsync(id, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _dbContextMock.Verify();
        }
    }
}
