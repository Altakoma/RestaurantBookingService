using BookingService.Infrastructure.Data.ApplicationDbContext;
using Moq;
using Moq.EntityFrameworkCore;

namespace BookingService.Tests.Mocks.DbContexts
{
    public class BookingServiceDbContextMock : Mock<BookingServiceDbContext>
    {
        public BookingServiceDbContextMock MockDataSet<T>(IQueryable<T> entities) where T : class
        {
            Setup(dbContext => dbContext.Set<T>())
            .ReturnsDbSet(entities)
            .Verifiable();

            return this;
        }

        public BookingServiceDbContextMock MockInsertAsync<T>(T entity) where T : notnull
        {
            Setup(dbContext => dbContext.AddAsync(entity, It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }

        public BookingServiceDbContextMock MockUpdate<T>(T entity) where T : notnull
        {
            Setup(dbContext => dbContext.Update(entity))
            .Verifiable();

            return this;
        }

        public BookingServiceDbContextMock MockDelete<T>(T entity) where T : notnull
        {
            Setup(dbContext => dbContext.Remove(entity))
            .Verifiable();

            return this;
        }

        public BookingServiceDbContextMock MockSaveChangesToDbAsync()
        {
            Setup(dbContext => dbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }
    }
}
