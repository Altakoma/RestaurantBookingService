using Bogus;
using CatalogService.Application.DTOs.FoodType;
using CatalogService.Domain.Entities;

namespace CatalogService.Tests.Fakers
{
    public class FoodTypeDataFaker
    {
        public const int StandartMaximumId = 15;
        public const int StandartMinimumId = 1;

        public static FoodType GetFakedFoodType(int id)
        {
            var faker = new Faker<FoodType>()
                .RuleFor(foodType => foodType.Id,
                faker => id)
                .RuleFor(foodType => foodType.Name,
                faker => faker.Random.Word());

            return faker.Generate();
        }

        public static FoodType GetFakedFoodType()
        {
            var faker = new Faker<FoodType>()
                .RuleFor(foodType => foodType.Id,
                faker => faker.Random.Number(StandartMinimumId, StandartMaximumId))
                .RuleFor(foodType => foodType.Name,
                faker => faker.Random.Word());

            return faker.Generate();
        }

        public static FoodType GetFakedFoodTypeForInsert()
        {
            var faker = new Faker<FoodType>()
                .RuleFor(foodType => foodType.Name,
                faker => faker.Random.Word());

            return faker.Generate();
        }

        public static FoodTypeDTO GetFakedFoodTypeDTO()
        {
            var faker = new Faker<FoodTypeDTO>()
                .RuleFor(foodTypeDTO => foodTypeDTO.Name,
                faker => faker.Random.Word());

            return faker.Generate();
        }
    }
}
