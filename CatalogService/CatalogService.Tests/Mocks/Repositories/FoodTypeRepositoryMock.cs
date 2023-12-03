using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Tests.Mocks.Repositories.Base;

namespace CatalogService.Tests.Mocks.Repositories
{
    public class FoodTypeRepositoryMock : BaseRepositoryMock<IFoodTypeRepository, FoodType>
    {
    }
}
