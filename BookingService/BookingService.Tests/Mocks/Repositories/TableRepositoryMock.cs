using BookingService.Application.Interfaces.Repositories;
using BookingService.Domain.Entities;
using BookingService.Tests.Mocks.Repositories.Base;
using Moq;

namespace BookingService.Tests.Mocks.Repositories
{
    public class TableRepositoryMock : BaseRepositoryMock<ITableRepository, Table>
    {
        public TableRepositoryMock MockGetRestaurantIdByTableIdAsync(int id, int restaurantId)
        {
            Setup(tableRepository =>
                tableRepository.GetRestaurantIdByTableIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(restaurantId)
            .Verifiable();

            return this;
        }
    }
}
