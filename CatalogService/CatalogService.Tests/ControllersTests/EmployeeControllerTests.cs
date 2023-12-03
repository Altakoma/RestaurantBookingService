using AutoMapper;
using CatalogService.Application.DTOs.Employee;
using CatalogService.Application.MappingProfiles;
using CatalogService.Domain.Entities;
using CatalogService.Presentation.Controllers;
using CatalogService.Tests.Fakers;
using CatalogService.Tests.Mocks.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CatalogService.Tests.ControllersTests
{
    public class EmployeeControllerTests
    {
        private readonly EmployeeServiceMock _employeeServiceMock;

        private readonly IMapper _mapper;
        private readonly EmployeeController _employeeController;

        public EmployeeControllerTests()
        {
            _employeeServiceMock = new();

            _mapper = new Mapper(new MapperConfiguration(configure =>
                configure.AddProfile(new EmployeeProfile())));

            _employeeController = new EmployeeController(_employeeServiceMock.Object);
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ReturnsReadEmployeeDTOs()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            var employees = new List<Employee> { employee };
            var readEmployeeDTOs = _mapper.Map<List<ReadEmployeeDTO>>(employees);

            _employeeServiceMock.MockGetAllAsync(readEmployeeDTOs);

            //Act
            var result = await _employeeController.GetAllEmployeesAsync(It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(readEmployeeDTOs);
        }

        [Fact]
        public async Task GetEmployeeAsync_ReturnsReadEmployeeDTO()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            var readEmployeeDTO = _mapper.Map<ReadEmployeeDTO>(employee);

            _employeeServiceMock.MockGetItemAsync(readEmployeeDTO);

            //Act
            var result = await _employeeController.GetEmployeeAsync(employee.Id,
                It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(readEmployeeDTO);
        }

        [Fact]
        public async Task InsertEmployeeAsync_ReturnsReadEmployeeDTO()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            var insertEmployeeDTO = _mapper.Map<InsertEmployeeDTO>(employee);
            var readEmployeeDTO = _mapper.Map<ReadEmployeeDTO>(employee);

            _employeeServiceMock.MockInsertEmployeeAsync(insertEmployeeDTO, 
                readEmployeeDTO);

            //Act
            var result = await _employeeController.InsertEmployee(insertEmployeeDTO,
                It.IsAny<CancellationToken>());
            var createdAtActionResult = result as CreatedAtActionResult;

            //Assert
            result.Should().BeOfType(typeof(CreatedAtActionResult));

            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult!.Value.Should().BeEquivalentTo(readEmployeeDTO);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_ReturnsNoContent()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();

            _employeeServiceMock.MockDeleteItemAsync(employee.Id);

            //Act
            var result = await _employeeController.DeleteEmployeeAsync(employee.Id,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeOfType(typeof(NoContentResult));
        }
    }
}
