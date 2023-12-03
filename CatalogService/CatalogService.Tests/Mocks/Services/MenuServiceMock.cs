using CatalogService.Application.DTOs.Menu;
using CatalogService.Application.Interfaces.Services;
using Moq;

namespace CatalogService.Tests.Mocks.Services
{
    public class MenuServiceMock : BaseServiceMock<IMenuService>
    {
        public MenuServiceMock MockInsertMenuAsync(InsertMenuDTO insertMenuDTO,
            ReadMenuDTO readMenuDTO)
        {
            Setup(employeeService => employeeService.InsertAsync<ReadMenuDTO>(
                insertMenuDTO, It.IsAny<CancellationToken>()))
            .ReturnsAsync(readMenuDTO)
            .Verifiable();

            return this;
        }
    }
}
