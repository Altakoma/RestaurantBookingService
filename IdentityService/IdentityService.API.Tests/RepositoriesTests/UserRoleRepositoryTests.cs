using AutoMapper;
using IdentityService.API.Tests.Fakers;
using IdentityService.API.Tests.RepositoriesTests.Base;
using IdentityService.BusinessLogic.DTOs.UserRole;
using IdentityService.BusinessLogic.MappingProfiles;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories;

namespace IdentityService.API.Tests.RepositoriesTests
{
    public class UserRoleRepositoryTests : BaseRepositoryTests<UserRole>
    {
        private readonly IMapper _mapper;

        public UserRoleRepositoryTests() : base()
        {
            _repository = new UserRoleRepository(_identityDbContextMock.Object,
                _mapperMock.Object);

            _mapper = new Mapper(new MapperConfiguration(
                configure => configure.AddProfile(new UserRoleProfile())));
        }

        [Fact]
        public async Task GetAllAsync_ReturnsReadUserRoleDTOs()
        {
            //Arrange
            var userRoles = new List<UserRole> { UserRoleDataFaker.GetFakedUserRole() };
            IQueryable<UserRole> userRoleQuery = userRoles.AsQueryable();

            var userReadDTOs = _mapper.Map<List<ReadUserRoleDTO>>(userRoles);

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(userRoleQuery, userReadDTOs);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsUserRoles()
        {
            //Arrange
            var userRoles = new List<UserRole> { UserRoleDataFaker.GetFakedUserRole() };
            IQueryable<UserRole> userRoleQuery = userRoles.AsQueryable();

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(userRoleQuery, userRoles);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsUserRole()
        {
            //Arrange
            var userRole = UserRoleDataFaker.GetFakedUserRole();
            IQueryable<UserRole> userRoleQuery = new List<UserRole> { userRole }.AsQueryable();

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(userRole.Id, userRoleQuery, userRole);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsReadUserRoleDTO()
        {
            //Arrange
            var userRole = UserRoleDataFaker.GetFakedUserRole();
            IQueryable<UserRole> userRoleQuery = new List<UserRole> { userRole }.AsQueryable();

            var readUserDTO = _mapper.Map<ReadUserRoleDTO>(userRole);

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(userRole.Id, userRoleQuery, readUserDTO);
        }
    }
}
