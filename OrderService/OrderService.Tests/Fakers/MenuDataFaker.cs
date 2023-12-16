using Bogus;
using OrderService.Application.DTOs.Menu;
using OrderService.Domain.Entities;

namespace OrderService.Tests.Fakers
{
    public static class MenuDataFaker
    {
        public static Menu GetFakedMenu()
        {
            var faker = new Faker<Menu>()
            .RuleFor(readMenuDTO => readMenuDTO.Id,
                faker => faker.Random.Number(min: 0, max: 20))
            .RuleFor(readMenuDTO => readMenuDTO.Cost,
                faker => faker.Random.Number(max: 500))
            .RuleFor(readMenuDTO => readMenuDTO.FoodName,
                faker => faker.Random.Word());

            return faker.Generate();
        }

        public static ReadMenuDTO GetFakedReadMenuDTO()
        {
            var faker = new Faker<ReadMenuDTO>()
            .RuleFor(readMenuDTO => readMenuDTO.Id,
                faker => faker.Random.Number(min: 0, max: 20))
            .RuleFor(readMenuDTO => readMenuDTO.Cost,
                faker => faker.Random.Number(max: 500))
            .RuleFor(readMenuDTO => readMenuDTO.FoodName,
                faker => faker.Random.Word());

            return faker.Generate();
        }

        public static ReadMenuDTO GetFakedReadMenuDTO(int id)
        {
            var faker = new Faker<ReadMenuDTO>()
            .RuleFor(readMenuDTO => readMenuDTO.Id,
                faker => id)
            .RuleFor(readMenuDTO => readMenuDTO.Cost,
                faker => faker.Random.Number(max: 500))
            .RuleFor(readMenuDTO => readMenuDTO.FoodName,
                faker => faker.Random.Word());

            return faker.Generate();
        }
    }
}
