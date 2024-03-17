using Moq;
using Moq.EntityFrameworkCore;
using OrderService.Infrastructure.Data.ApplicationSQLDbContext;

namespace OrderService.Tests.Mocks.DbContexts
{
    public class OrderServiceSqlDbContextMock : Mock<OrderServiceSqlDbContext>
    {
        public OrderServiceSqlDbContextMock MockDataSet<T>(IQueryable<T> entities) where T : class
        {
            Setup(dbContext => dbContext.Set<T>())
            .ReturnsDbSet(entities)
            .Verifiable();

            return this;
        }

        public OrderServiceSqlDbContextMock MockInsertAsync<T>(T entity) where T : notnull
        {
            Setup(dbContext => dbContext.AddAsync(entity, It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }

        public OrderServiceSqlDbContextMock MockUpdate<T>(T entity) where T : notnull
        {
            Setup(dbContext => dbContext.Update(entity))
            .Verifiable();

            return this;
        }

        public OrderServiceSqlDbContextMock MockDelete<T>(T entity) where T : notnull
        {
            Setup(dbContext => dbContext.Remove(entity))
            .Verifiable();

            return this;
        }

        public OrderServiceSqlDbContextMock MockSaveChangesToDbAsync()
        {
            Setup(dbContext => dbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }
    }
}
