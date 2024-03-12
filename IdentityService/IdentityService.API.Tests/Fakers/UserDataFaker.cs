using Bogus;
using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.BusinessLogic.DTOs.UserRole;
using IdentityService.DataAccess.Entities;

namespace IdentityService.API.Tests.Fakers
{
    internal static class UserDataFaker
    {
        public const int StandartMaximumId = 15;
        public const int StandartMinimumId = 1;

        internal static User GetFakedUser(int id)
        {
            var faker = new Faker<User>()
                .RuleFor(user => user.Id,
                        faker => id)
                .RuleFor(user => user.UserRoleId,
                        faker => 2)
                .RuleFor(user => user.Password,
                        faker => faker.Random.Words(3).ToLower())
                .RuleFor(user => user.Name,
                        faker => faker.Name.FirstName())
                .RuleFor(user => user.Login,
                        faker => faker.Random.Words(2).ToLower());

            var user = faker.Generate();

            user.UserRole = UserRoleDataFaker.GetFakedUserRole();

            return user;
        }

        internal static InsertUserDTO GetFakedInsertUserDTO()
        {
            var faker = new Faker<InsertUserDTO>()
                .RuleFor(insertUserDTO => insertUserDTO.UserRoleId,
                        faker => 2)
                .RuleFor(insertUserDTO => insertUserDTO.Password,
                        faker => faker.Random.Words(3).ToLower())
                .RuleFor(insertUserDTO => insertUserDTO.Name,
                        faker => faker.Name.FirstName())
                .RuleFor(insertUserDTO => insertUserDTO.Login,
                        faker => faker.Random.Words(2).ToLower());

            return faker.Generate();
        }

        internal static UpdateUserDTO GetFakedUpdateUserDTO()
        {
            var faker = new Faker<UpdateUserDTO>()
                .RuleFor(updateUserDTO => updateUserDTO.UserRoleId,
                        faker => 2)
                .RuleFor(updateUserDTO => updateUserDTO.Password,
                        faker => faker.Random.Words(3).ToLower())
                .RuleFor(updateUserDTO => updateUserDTO.Name,
                        faker => faker.Name.FirstName())
                .RuleFor(updateUserDTO => updateUserDTO.Login,
                        faker => faker.Random.Words(2).ToLower());

            return faker.Generate();
        }

        internal static ReadUserDTO GetFakedReadUserDTO(int id, string login)
        {
            var faker = new Faker<ReadUserDTO>()
                .RuleFor(readUserDTO => readUserDTO.UserRoleName,
                        faker => "Client")
                .RuleFor(readUserDTO => readUserDTO.Id,
                        faker => id)
                .RuleFor(readUserDTO => readUserDTO.Name,
                        faker => faker.Name.FirstName())
                .RuleFor(readUserDTO => readUserDTO.Login,
                        faker => login);

            return faker.Generate();
        }

        internal static ReadUserDTO GetFakedReadUserDTO(string login)
        {
            int id = GetRandomNumber(10);

            return GetFakedReadUserDTO(id, login);
        }

        internal static ReadUserDTO GetFakedReadUserDTO(int id)
        {
            var faker = new Faker<ReadUserDTO>()
                .RuleFor(readUserDTO => readUserDTO.Login,
                faker => faker.Random.Word());

            ReadUserDTO readUserDTO = faker.Generate();

            return GetFakedReadUserDTO(id, readUserDTO.Login);
        }

        internal static ReadUserDTO GetFakedReadUserDTO()
        {
            int id = GetRandomNumber(10);

            return GetFakedReadUserDTO(id);
        }

        internal static ReadUserRoleDTO GetFakedReadUserRoleDTO(int id)
        {
            var faker = new Faker<ReadUserRoleDTO>()
                .RuleFor(readUserRoleDTO => readUserRoleDTO.Name,
                        faker => faker.Random.Word());

            return faker.Generate();
        }

        internal static ReadUserRoleDTO GetFakedReadUserRoleDTO()
        {
            Random random = new Random();
            int id = random.Next(10);

            return GetFakedReadUserRoleDTO(id);
        }

        internal static AccessTokenDTO GetFakedAccessTokenDTO()
        {
            var faker = new Faker<AccessTokenDTO>()
                .RuleFor(accessTokenDTO => accessTokenDTO.EncodedToken,
                        faker => faker.Random.Words());

            return faker.Generate();
        }

        internal static string FakeRandomString(int maximum)
        {
            var word = new Faker().Random.Words(maximum);

            return word;
        }

        internal static int GetRandomNumber(int maximum)
        {
            Random random = new Random();

            return random.Next(StandartMinimumId, maximum);
        }
    }
}
