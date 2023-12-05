using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Tests.Mocks.Repositories.Base;
using Moq;

namespace CatalogService.Tests.Mocks.Repositories
{
    public class RestaurantRepositoryMock : BaseRepositoryMock<IRestaurantRepository, Restaurant>
    {
        public RestaurantRepositoryMock MockIsWorkingAtRestaurant(int subjectId,
            int restaurantId, bool isWorkingAtRestaurant)
        {
            Setup(restaurantRepository => restaurantRepository.IsWorkingAtRestaurantAsync(
                subjectId, restaurantId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(isWorkingAtRestaurant)
            .Verifiable();

            return this;
        }
    }
}
