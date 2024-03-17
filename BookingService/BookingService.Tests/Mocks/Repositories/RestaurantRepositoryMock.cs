using BookingService.Application.Interfaces.Repositories;
using BookingService.Domain.Entities;
using BookingService.Tests.Mocks.Repositories.Base;

namespace BookingService.Tests.Mocks.Repositories
{
    public class RestaurantRepositoryMock : BaseRepositoryMock<IRestaurantRepository, Restaurant>
    {
    }
}
