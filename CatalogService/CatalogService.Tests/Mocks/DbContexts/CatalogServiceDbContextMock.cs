using CatalogService.Infrastructure.Data.ApplicationDbContext;
using Moq;
using Moq.EntityFrameworkCore;

namespace CatalogService.Tests.Mocks.DbContexts
{
    public class CatalogServiceDbContextMock : Mock<CatalogServiceDbContext>
    {
        public CatalogServiceDbContextMock MockDataSet<T>(IQueryable<T> entities) where T : class
        {
            Setup(dbContext => dbContext.Set<T>())
            .ReturnsDbSet(entities)
            .Verifiable();

            return this;
        }

        public CatalogServiceDbContextMock MockInsertAsync<T>(T entity) where T : notnull
        {
            Setup(dbContext => dbContext.AddAsync(entity, It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }

        public CatalogServiceDbContextMock MockUpdate<T>(T entity) where T : notnull
        {
            Setup(dbContext => dbContext.Update(entity))
            .Verifiable();

            return this;
        }

        public CatalogServiceDbContextMock MockDelete<T>(T entity) where T : notnull
        {
            Setup(dbContext => dbContext.Remove(entity))
            .Verifiable();

            return this;
        }

        public CatalogServiceDbContextMock MockSaveChangesToDbAsync(int saved)
        {
            Setup(dbContext => dbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(saved)
            .Verifiable();

            return this;
        }
    }
}
