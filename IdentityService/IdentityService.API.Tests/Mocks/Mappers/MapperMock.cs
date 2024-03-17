using AutoMapper;
using Moq;
using Moq.EntityFrameworkCore.DbAsyncQueryProvider;

namespace IdentityService.API.Tests.Mocks.Mappers
{
    public class MapperMock : Mock<IMapper>
    {
        public MapperMock MockProjectTo<U, T>(IQueryable<T> query, ICollection<U> result)
        {
            Setup(mapper => mapper.ProjectTo<U>(query, It.IsAny<object>()))
            .Returns(new InMemoryAsyncEnumerable<U>(result))
            .Verifiable();

            return this;
        }
    }
}
