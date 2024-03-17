using IdentityService.DataAccess.DatabaseContext;
using IdentityService.DataAccess.Entities;
using Moq;
using Moq.EntityFrameworkCore;

namespace IdentityService.API.Tests.Mocks.DbContexts
{
    public class IdentityDbContextMock : Mock<IdentityDbContext>
    {
        public IdentityDbContextMock MockDataSet<T>(IQueryable<T> entities) where T : class
        {
            Setup(dbContext => dbContext.Set<T>())
            .ReturnsDbSet(entities)
            .Verifiable();

            return this;
        }

        public IdentityDbContextMock MockDataUsers(IQueryable<User> users)
        {
            Setup(dbContext => dbContext.Users)
            .ReturnsDbSet(users)
            .Verifiable();

            return this;
        }

        public IdentityDbContextMock MockInsertAsync<T>(T entity) where T : notnull
        {
            Setup(dbContext => dbContext.AddAsync(entity, It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }

        public IdentityDbContextMock MockUpdate<T>(T entity) where T : notnull
        {
            Setup(dbContext => dbContext.Update(entity))
            .Verifiable();

            return this;
        }

        public IdentityDbContextMock MockDelete<T>(T entity) where T : notnull
        {
            Setup(dbContext => dbContext.Remove(entity))
            .Verifiable();

            return this;
        }

        public IdentityDbContextMock MockSaveChangesToDbAsync(int saved)
        {
            Setup(dbContext => dbContext.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(saved)
            .Verifiable();

            return this;
        }
    }
}
