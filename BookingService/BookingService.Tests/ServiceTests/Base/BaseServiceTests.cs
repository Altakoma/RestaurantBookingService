using BookingService.Application.Interfaces.Repositories.Base;
using BookingService.Domain.Entities;
using BookingService.Domain.Exceptions;
using BookingService.Domain.Interfaces.Services.Base;
using BookingService.Tests.Mocks.Mappers;
using BookingService.Tests.Mocks.Repositories.Base;
using FluentAssertions;
using Moq;

namespace BookingService.Tests.ServiceTests.Base
{
    public class BaseServiceTests<TBaseRepository, TEntity> where TBaseRepository : class, IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly MapperMock _mapperMock;

        protected BaseRepositoryMock<TBaseRepository, TEntity> _baseRepositoryMock;
        protected IBaseService _baseService;

        public BaseServiceTests()
        {
            _mapperMock = new();

            _baseRepositoryMock = default!;
            _baseService = default!;
        }
        public async Task GetByIdAsync_ReturnsEntity<T>(
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

        public async Task InsertAsync_ReturnsEntity<U, T>(U insertEntity,
            TEntity entity, T readEntity)
        {
            //Arrange
            _mapperMock.MockMap(insertEntity, entity);

            _baseRepositoryMock.MockInsertAsync(entity, readEntity);

            //Act
            var result = await _baseService.InsertAsync<U, T>(insertEntity,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readEntity);

            _mapperMock.Verify();
            _baseRepositoryMock.Verify();
        }

        public async Task Update_ReturnsEntity<U, T>(U updateEntity,
            TEntity entity, T readEntity)
        {
            //Arrange
            _mapperMock.MockRefMap(updateEntity, entity);

            _baseRepositoryMock.MockGetByIdAsync(entity.Id, entity)
                .MockUpdateAsync(entity, readEntity);

            //Act
            var result = await _baseService.UpdateAsync<U, T>(entity.Id, updateEntity,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readEntity);

            _mapperMock.Verify();
            _baseRepositoryMock.Verify();
        }

        public async Task DeleteAsync_WhenItIsSaved(TEntity entity)
        {
            //Arrange
            bool isSaved = true;

            _baseRepositoryMock.MockDeleteAsync(entity.Id)
                               .MockSaveChangesAsync(isSaved);

            //Act
            await _baseService.DeleteAsync(entity.Id, It.IsAny<CancellationToken>());

            //Assert
            _baseRepositoryMock.Verify();
        }

        public async Task DeleteAsync_WhenItIsNotSaved_ThrowsDbOperationException(TEntity entity)
        {
            //Arrange
            bool isSaved = false;

            _baseRepositoryMock.MockDeleteAsync(entity.Id)
                               .MockSaveChangesAsync(isSaved);

            //Act
            var result = _baseService.DeleteAsync(entity.Id,
                It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<DbOperationException>(() => result);

            _baseRepositoryMock.Verify();
        }
    }
}
