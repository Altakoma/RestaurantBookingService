using Bogus;
using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.BusinessLogic.DTOs.UserRole;
using IdentityService.DataAccess.Entities;

namespace IdentityService.API.Tests.Fakers
{
    public static class UserDataFaker
    {
        public const int StandartMaximumId = 15;
        public const int StandartMinimumId = 1;

        public static User GetFakedUser(int id)
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

        public static User GetFakedUser()
        {
            var faker = new Faker<User>()
                .RuleFor(user => user.UserRoleId,
                        faker => 2)
                .RuleFor(user => user.Password,
                        faker => faker.Random.Words(1).ToLower())
                .RuleFor(user => user.Name,
                        faker => faker.Name.FirstName())
                .RuleFor(user => user.Login,
                        faker => faker.Random.Words(2).ToLower());

            return faker.Generate();
        }

        public static InsertUserDTO GetFakedInsertUserDTO()
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

        public static UpdateUserDTO GetFakedUpdateUserDTO()
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

        public static ReadUserDTO GetFakedReadUserDTO(int id, string login)
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

        public static ReadUserDTO GetFakedReadUserDTO(string login)
        {
            int id = GetRandomNumber(10);

            return GetFakedReadUserDTO(id, login);
        }

        public static ReadUserDTO GetFakedReadUserDTO(int id)
        {
            var faker = new Faker<ReadUserDTO>()
                .RuleFor(readUserDTO => readUserDTO.Login,
                faker => faker.Random.Word());

            ReadUserDTO readUserDTO = faker.Generate();

            return GetFakedReadUserDTO(id, readUserDTO.Login);
        }

        public static ReadUserDTO GetFakedReadUserDTO()
        {
            int id = GetRandomNumber(10);

            return GetFakedReadUserDTO(id);
        }

        public static ReadUserRoleDTO GetFakedReadUserRoleDTO(int id)
        {
            var faker = new Faker<ReadUserRoleDTO>()
                .RuleFor(readUserRoleDTO => readUserRoleDTO.Name,
                        faker => faker.Random.Word());

            return faker.Generate();
        }

        public static ReadUserRoleDTO GetFakedReadUserRoleDTO()
        {
            Random random = new Random();
            int id = random.Next(10);

            return GetFakedReadUserRoleDTO(id);
        }

        public static AccessTokenDTO GetFakedAccessTokenDTO()
        {
            var faker = new Faker<AccessTokenDTO>()
                .RuleFor(accessTokenDTO => accessTokenDTO.EncodedToken,
                        faker => faker.Random.Words());

            return faker.Generate();
        }

        public static string FakeRandomString(int maximum)
        {
            var word = new Faker().Random.Words(maximum);

            return word;
        }

        public static int GetRandomNumber(int maximum)
        {
            Random random = new Random();

            return random.Next(StandartMinimumId, maximum);
        }
    }
}
