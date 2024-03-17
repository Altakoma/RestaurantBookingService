using CatalogService.Application.DTOs.Employee;
using CatalogService.Application.Interfaces.Services;
using Moq;

namespace CatalogService.Tests.Mocks.Services
{
    public class EmployeeServiceMock : BaseServiceMock<IEmployeeService>
    {
        public EmployeeServiceMock MockInsertEmployeeAsync(InsertEmployeeDTO insertEmployeeDTO,
            ReadEmployeeDTO readEmployeeDTO)
        {
            Setup(employeeService => employeeService.InsertAsync<ReadEmployeeDTO>(
                insertEmployeeDTO, It.IsAny<CancellationToken>()))
            .ReturnsAsync(readEmployeeDTO)
            .Verifiable();

            return this;
        }
    }
}
