using Bogus;
using CatalogService.Application.DTOs.Menu;
using CatalogService.Domain.Entities;

namespace CatalogService.Tests.Fakers
{
    public static class MenuDataFaker
    {
        public const int StandartMaximumId = 15;
        public const int StandartMaximumCost = 1000;
        public const int StandartMinimumId = 1;

        public static Menu GetFakedMenu()
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

        public static Menu GetFakedMenuForInsert()
        {
            var faker = new Faker<Menu>()
                .RuleFor(menu => menu.FoodName,
                faker => faker.Random.Word())
                .RuleFor(menu => menu.FoodType,
                faker => FoodTypeDataFaker.GetFakedFoodTypeForInsert())
                .RuleFor(menu => menu.Restaurant,
                faker => RestaurantDataFaker.GetFakedRestaurantForInsert())
                .RuleFor(menu => menu.Cost,
                faker => faker.Random.Double() + faker.Random.UInt(max: StandartMaximumCost));

            return faker.Generate();
        }

        public static InsertMenuDTO GetFakedInsertMenuDTO()
        {
            var faker = new Faker<InsertMenuDTO>()
                .RuleFor(menu => menu.FoodName,
                faker => faker.Random.Word())
                .RuleFor(menu => menu.FoodTypeId,
                faker => faker.Random.Number(StandartMinimumId, StandartMaximumId))
                .RuleFor(menu => menu.RestaurantId,
                faker => faker.Random.Number(StandartMinimumId, StandartMaximumId))
                .RuleFor(menu => menu.Cost,
                faker => faker.Random.Double() + faker.Random.UInt(max: StandartMaximumCost));

            return faker.Generate();
        }

        public static InsertMenuDTO GetFakedInsertMenuDTO(int restaurantId,
            int foodTypeId)
        {
            var faker = new Faker<InsertMenuDTO>()
                .RuleFor(menu => menu.FoodName,
                faker => faker.Random.Word())
                .RuleFor(menu => menu.FoodTypeId,
                faker => foodTypeId)
                .RuleFor(menu => menu.RestaurantId,
                faker => restaurantId)
                .RuleFor(menu => menu.Cost,
                faker => faker.Random.Double() + faker.Random.UInt(max: StandartMaximumCost));

            return faker.Generate();
        }

        public static UpdateMenuDTO GetFakedUpdateMenuDTO()
        {
            var faker = new Faker<UpdateMenuDTO>()
                .RuleFor(menu => menu.FoodName,
                faker => faker.Random.Word())
                .RuleFor(menu => menu.FoodTypeId,
                faker => faker.Random.Number(StandartMinimumId, StandartMaximumId))
                .RuleFor(menu => menu.RestaurantId,
                faker => faker.Random.Number(StandartMinimumId, StandartMaximumId))
                .RuleFor(menu => menu.Cost,
                faker => faker.Random.Double() + faker.Random.UInt(max: StandartMaximumCost));

            return faker.Generate();
        }
    }
}
