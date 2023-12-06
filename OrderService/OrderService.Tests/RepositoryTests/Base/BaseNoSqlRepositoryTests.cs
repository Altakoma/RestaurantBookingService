using FluentAssertions;
using MongoDB.Driver;
using Moq;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Repositories.Base;

namespace OrderService.Tests.RepositoryTests.Base
{
    public class BaseNoSqlRepositoryTests<T> where T : BaseEntity
    {
        protected readonly Mock<IMongoCollection<T>> _collectionMock;
        protected BaseNoSqlRepository<T> _baseNoSqlRepository;

        public BaseNoSqlRepositoryTests()
        {
            _collectionMock = new();

            _baseNoSqlRepository = default!;
        }

        public async Task GetAllAsync_ShouldReturnItems(List<T> expected)
        {
            // Arrange
            var asyncCursor = new Mock<IAsyncCursor<T>>();

            asyncCursor.SetupSequence(_async => _async.MoveNext(default)).Returns(true).Returns(false);
            asyncCursor.SetupGet(_async => _async.Current).Returns(expected);

            _collectionMock.Setup(collection =>
                collection.Find(It.IsAny<FilterDefinition<T>>(), It.IsAny<FindOptions>()))
            .Returns(MockFind(expected));

            // Act
            var result = await _baseNoSqlRepository.GetAllAsync(0,
                0, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        private IFindFluent<T, T> MockFind(List<T> expectedCollection)
        {
            var findFluentMock = new Mock<IFindFluent<T, T>>();

            findFluentMock.Setup(x => x.Skip(It.IsAny<int>())).Returns(findFluentMock.Object);
            findFluentMock.Setup(x => x.Limit(It.IsAny<int>())).Returns(findFluentMock.Object);
            findFluentMock.Setup(x => x.ToListAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedCollection);

            return findFluentMock.Object;
        }
    }
}
