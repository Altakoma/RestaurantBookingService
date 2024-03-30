using FluentAssertions;
using IdentityService.API.Controllers;
using IdentityService.API.Tests.Fakers;
using IdentityService.BusinessLogic.DTOs.UserRole;
using IdentityService.DataAccess.DatabaseContext;
using IdentityService.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.API.IntegrationTests
{
    public class UserRoleControllerTests : IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly IServiceScope _serviceScope;

        private readonly UserRoleController _userRoleController;

        private readonly CancellationTokenSource _cancellationTokenSource;

        private readonly IdentityDbContext _identityDbContext;

        public UserRoleControllerTests(IntegrationTestWebAppFactory factory)
        {
            _serviceScope = factory.Services.CreateScope();

            _userRoleController = _serviceScope.ServiceProvider
                .GetRequiredService<UserRoleController>();

            _identityDbContext = _serviceScope.ServiceProvider
                .GetRequiredService<IdentityDbContext>();

            _cancellationTokenSource = new CancellationTokenSource();
        }

        [Fact]
        public async Task GetAllUserRolesAsync_ReturnsReadUserRoleDTOs()
        {
            //Arrange
            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _userRoleController.GetAllUserRolesAsync(cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult!.Value.Should().BeOfType<List<ReadUserRoleDTO>>();
        }

        [Fact]
        public async Task GetUserRoleByIdAsync_ReturnsReadUserRoleDTO()
        {
            //Arrange
            UserRole userRole = UserRoleDataFaker.GetFakedUserRole();
            userRole.Id = 0;

            _identityDbContext.UserRoles.Add(userRole);
            _identityDbContext.SaveChanges();

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _userRoleController.GetUserRoleByIdAsync(userRole.Id,
                cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult!.Value.Should().BeEquivalentTo(userRole, options =>
                options.ExcludingNestedObjects().ExcludingMissingMembers());
        }
    }
}
