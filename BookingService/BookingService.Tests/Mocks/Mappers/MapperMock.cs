using AutoMapper;
using Moq;
using Moq.EntityFrameworkCore.DbAsyncQueryProvider;

namespace BookingService.Tests.Mocks.Mappers
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

        public MapperMock MockMap<U, T>(U entity, T mappedEntity)
        {
            Setup(mapper => mapper.Map<T>(entity))
            .Returns(mappedEntity)
            .Verifiable();

            return this;
        }

        public MapperMock MockRefMap<U, T>(U entity, T mappedEntity)
        {
            Setup(mapper => mapper.Map(entity, mappedEntity))
            .Verifiable();

            return this;
        }
    }
}
