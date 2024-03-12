using FluentAssertions;
using IdentityService.API.Tests.Mocks.DbContexts;
using IdentityService.API.Tests.Mocks.Mappers;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Interfaces.Base;
using Moq;

namespace IdentityService.API.Tests.RepositoriesTests.Base
{
    public class BaseRepositoryTests<T> where T : BaseEntity
    {
        protected readonly IdentityDbContextMock _identityDbContextMock;
        protected readonly MapperMock _mapperMock;

        protected IRepository<T> _repository;

        public BaseRepositoryTests()
        {
            _identityDbContextMock = new();

            _mapperMock = new();
        }

        public async Task GetAllAsync_ReturnsEntities<U>(IQueryable<T> query,
            ICollection<U> resultCollection)
        {
            //Arrange
            _identityDbContextMock.MockDataSet(query);

            _mapperMock.MockProjectTo(query, resultCollection);

            //Act
            var result = await _repository.GetAllAsync<U>(It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(resultCollection);

            _identityDbContextMock.Verify();
            _mapperMock.Verify();
        }

        public async Task GetByIdAsync_ReturnsEntity<U>(int id, IQueryable<T> query,
            U resultObject)
        {
            //Arrange
            ICollection<U> resultCollection = new List<U> { resultObject };

            _identityDbContextMock.MockDataSet(query);

            _mapperMock.MockProjectTo(query, resultCollection);

            //Act
            var result = await _repository.GetByIdAsync<U>(id, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(resultObject);

            _identityDbContextMock.Verify();
            _mapperMock.Verify();
        }

        public async Task InsertAsync_SuccessfullyExecuted(T entity)
        {
            //Arrange
            _identityDbContextMock.MockInsertAsync(entity);

            //Act
            var result = await _repository.InsertAsync(entity, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(entity);

            _identityDbContextMock.Verify(
                dbContext => dbContext.AddAsync(entity, It.IsAny<CancellationToken>()));
        }

        public void UpdateEntity_SuccessfullyExecuted(T entity)
        {
            //Arrange
            _identityDbContextMock.MockUpdate(entity);

            //Act
            _repository.Update(entity);

            //Assert
            _identityDbContextMock.Verify(dbContext => dbContext.Update(entity));
        }

        public void DeleteEntity_SuccessfullyExecuted(T entity)
        {
            //Arrange
            _identityDbContextMock.MockDelete(entity);

            //Act
            _repository.Delete(entity);

            //Assert
            _identityDbContextMock.Verify(dbContext => dbContext.Remove(entity));
        }
    }
}
