using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Tests.Mocks.Repositories.Base;
using Moq;

namespace CatalogService.Tests.Mocks.Repositories
{
    public class EmployeeRepositoryMock : BaseRepositoryMock<IEmployeeRepository, Employee>
    {
        public EmployeeRepositoryMock MockGetAllByRestaurantIdAsync<T>(int id,
            ICollection<T> employees)
        {
            Setup(employeeRepository => employeeRepository.GetAllByRestaurantIdAsync<T>(id
                , It.IsAny<CancellationToken>()))
            .ReturnsAsync(employees)
            .Verifiable();

            return this;
        }

        public EmployeeRepositoryMock MockIsExistingAsync(int employeeId,
            bool isExisting)
        {
            Setup(employeeRepository => employeeRepository.IsExistingAsync(
                employeeId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(isExisting)
            .Verifiable();

            return this;
        }
    }
}
