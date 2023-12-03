using Bogus;
using IdentityService.BusinessLogic.DTOs.UserRole;
using IdentityService.DataAccess.Entities;

namespace IdentityService.API.Tests.Fakers
{
    internal static class UserRoleDataFaker
    {
        internal static ReadUserRoleDTO GetFakedReadUserRoleDTO()
        {
            var faker = new Faker<ReadUserRoleDTO>()
                .RuleFor(readUserRoleDTO => readUserRoleDTO.Id,
                faker => faker.Random.Number(min: 0, max: UserDataFaker.StandartMaximumId))
                .RuleFor(readUserRoleDTO => readUserRoleDTO.Name,
                faker => faker.Random.Word());

            return faker.Generate();
        }

        internal static UserRole GetFakedUserRole()
        {
            var faker = new Faker<UserRole>()
                .RuleFor(userRole => userRole.Id,
                faker => faker.Random.Number(min: 0, max: UserDataFaker.StandartMaximumId))
                .RuleFor(userRole => userRole.Name,
                faker => faker.Random.Word());

            return faker.Generate();
        }
    }
}
