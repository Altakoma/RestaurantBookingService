using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Tests.Mocks.Repositories.Base;
using Moq;

namespace CatalogService.Tests.Mocks.Repositories
{
    public class MenuRepositoryMock : BaseRepositoryMock<IMenuRepository, Menu>
    {
    }
}
