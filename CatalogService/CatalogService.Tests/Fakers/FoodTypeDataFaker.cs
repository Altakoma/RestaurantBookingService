using Bogus;
using CatalogService.Domain.Entities;

namespace CatalogService.Tests.Fakers
{
    internal class FoodTypeDataFaker
    {
        public const int StandartMaximumId = 15;
        public const int StandartMinimumId = 1;

        internal static FoodType GetFakedFoodType(int id)
        {
            var faker = new Faker<FoodType>()
                .RuleFor(foodType => foodType.Id,
                faker => id)
                .RuleFor(foodType => foodType.Name,
                faker => faker.Random.Word());

            return faker.Generate();
        }

        internal static FoodType GetFakedFoodType()
        {
            var faker = new Faker<FoodType>()
                .RuleFor(foodType => foodType.Id,
                faker => faker.Random.Number(StandartMinimumId, StandartMaximumId))
                .RuleFor(foodType => foodType.Name,
                faker => faker.Random.Word());

            return faker.Generate();
        }
    }
}
