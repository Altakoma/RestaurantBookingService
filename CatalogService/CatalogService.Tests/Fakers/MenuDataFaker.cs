using Bogus;
using CatalogService.Domain.Entities;

namespace CatalogService.Tests.Fakers
{
    internal static class MenuDataFaker
    {
        public const int StandartMaximumId = 15;
        public const int StandartMaximumCost = 1000;
        public const int StandartMinimumId = 1;

        internal static Menu GetFakedMenu()
        {
            var faker = new Faker<Menu>()
                .RuleFor(menu => menu.Id,
                faker => faker.Random.Number(StandartMinimumId, StandartMaximumId))
                .RuleFor(menu => menu.FoodName,
                faker => faker.Random.Word())
                .RuleFor(menu => menu.FoodTypeId,
                faker => faker.Random.Number(StandartMinimumId, StandartMaximumId))
                .RuleFor(menu => menu.RestaurantId,
                faker => faker.Random.Number(StandartMinimumId, StandartMaximumId))
                .RuleFor(menu => menu.Cost,
                faker => faker.Random.Double() + faker.Random.UInt(max: StandartMaximumCost));

            Menu menu = faker.Generate();

            menu.FoodType = FoodTypeDataFaker.GetFakedFoodType(menu.FoodTypeId);
            menu.Restaurant = RestaurantDataFaker.GetFakedRestaurant(menu.RestaurantId);

            return menu;
        }
    }
}
