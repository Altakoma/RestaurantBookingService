using CatalogService.Application.Interfaces.Repositories.Base;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Domain.Interfaces.Services.Base;
using CatalogService.Tests.Mocks.Mappers;
using CatalogService.Tests.Mocks.Repositories.Base;
using FluentAssertions;
using Moq;

namespace CatalogService.Tests.ServicesTests.Base
{
    public class BaseServiceTests<TRepository, TEntity>
        where TEntity : BaseEntity
        where TRepository : class, IRepository<TEntity>
    {
        protected readonly MapperMock _mapperMock;

        protected BaseRepositoryMock<TRepository, TEntity> _baseRepositoryMock;
        protected IBaseService _baseService;

        public BaseServiceTests()
        {
            _mapperMock = new();

            _baseRepositoryMock = default!;
            _baseService = default!;
        }

        public async Task GetByIdAsync_WhenItIsExisting_ReturnsEntity<T>(
            int id, T entity)
        {
            //Arrange
            _baseRepositoryMock.MockGetByIdAsync(id, entity);

            //Act
            var result = await _baseService.GetByIdAsync<T>(id,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(entity);

            _baseRepositoryMock.Verify();
        }

        public async Task GetByIdAsync_WhenItIsNotExisting_ThrowsNotFoundException<T>(
            int id)
        {
            //Arrange
            _baseRepositoryMock.MockGetByIdAsync(id, default(T));

            //Act
            var result = _baseService.GetByIdAsync<T>(id,
                It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _baseRepositoryMock.Verify();
        }

        public async Task GetAllAsync_ReturnsEntities<T>(ICollection<T> entities)
        {
            //Arrange
            _baseRepositoryMock.MockGetAllAsync(entities);

            //Act
            var result = await _baseService.GetAllAsync<T>(It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(entities);

            _baseRepositoryMock.Verify();
        }

        public async Task InsertAsync_WhenItIsSaved_ReturnsEntity<U, T>(U insertEntity,
            TEntity entity, T readEntity)
        {
            //Arrange
            bool isSaved = true;

            _mapperMock.MockMap(insertEntity, entity);

            _baseRepositoryMock.MockInsertAsync(entity)
                               .MockSaveChangesAsync(isSaved);

            //Act
            var result = await _baseService.InsertAsync<U, T>(insertEntity,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readEntity);

            _mapperMock.Verify();
            _baseRepositoryMock.Verify();
        }

        public async Task InsertAsync_WhenItIsNotSaved_ThrowsDbOperationException<U, T>(U insertEntity,
            TEntity entity)
        {
            //Arrange
            bool isSaved = false;

            _mapperMock.MockMap(insertEntity, entity);

            _baseRepositoryMock.MockInsertAsync(entity)
                               .MockSaveChangesAsync(isSaved);

            //Act
            var result = _baseService.InsertAsync<U, T>(insertEntity,
                It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<DbOperationException>(() => result);

            _mapperMock.Verify();
            _baseRepositoryMock.Verify();
        }

        public async Task Update_WhenItIsSaved_ReturnsEntity<U, T>(U updateEntity,
            TEntity entity, T readEntity)
        {
            //Arrange
            bool isSaved = true;

            _mapperMock.MockMap(updateEntity, entity);

            _baseRepositoryMock.MockUpdate(entity)
                               .MockSaveChangesAsync(isSaved)
                               .MockGetByIdAsync(entity.Id, readEntity);

            //Act
            var result = await _baseService.UpdateAsync<U, T>(entity.Id, updateEntity,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readEntity);

            _mapperMock.Verify();
            _baseRepositoryMock.Verify();
        }

        public async Task Update_WhenItIsNotSaved_ThrowsDbOperationException<U, T>(U updateEntity,
            TEntity entity, T readEntity)
        {
            //Arrange
            bool isSaved = false;

            _mapperMock.MockMap(updateEntity, entity);

            _baseRepositoryMock.MockUpdate(entity)
                               .MockSaveChangesAsync(isSaved)
                               .MockGetByIdAsync(entity.Id, readEntity);

            //Act
            var result = _baseService.UpdateAsync<U, T>(entity.Id, updateEntity,
                It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<DbOperationException>(() => result);

            _mapperMock.Verify();
            _baseRepositoryMock.Verify();
        }

        public async Task Update_WhenItIsNotFound_ThrowsNotFoundException<U, T>(U updateEntity,
            int id)
        {
            //Arrange
            _baseRepositoryMock.MockGetByIdAsync(id, default(T));

            //Act
            var result = _baseService.UpdateAsync<U, T>(id, updateEntity,
                It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _baseRepositoryMock.Verify();
        }

        public async Task DeleteAsync_WhenItIsSaved_ReturnsId(TEntity entity)
        {
            //Arrange
            bool isSaved = true;

            _baseRepositoryMock.MockDeleteAsync(entity.Id)
                               .MockSaveChangesAsync(isSaved)
                               .MockGetByIdAsync(entity.Id, entity);

            //Act
            var result = await _baseService.DeleteAsync(entity.Id,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().Be(entity.Id);

            _baseRepositoryMock.Verify();
        }

        public async Task DeleteAsync_WhenItIsNotSaved_ThrowsDbOperationException(TEntity entity)
        {
            //Arrange
            bool isSaved = false;

            _baseRepositoryMock.MockDeleteAsync(entity.Id)
                               .MockSaveChangesAsync(isSaved)
                               .MockGetByIdAsync(entity.Id, entity);

            //Act
            var result = _baseService.DeleteAsync(entity.Id,
                It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<DbOperationException>(() => result);

            _baseRepositoryMock.Verify();
        }

        public async Task DeleteAsync_WhenItIsNotExisting_ThrowsNotFoundException(int id)
        {
            //Arrange
            _baseRepositoryMock.MockGetByIdAsync(id, default(TEntity));

            //Act
            var result = _baseService.DeleteAsync(id, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _baseRepositoryMock.Verify();
        }
    }
}
