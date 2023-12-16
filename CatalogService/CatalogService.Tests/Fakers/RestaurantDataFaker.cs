using Bogus;
using CatalogService.Application.DTOs.Restaurant;
using CatalogService.Domain.Entities;

namespace CatalogService.Tests.Fakers
{
    public class RestaurantDataFaker
    {
        public const int StandartMaximumId = 15;
        public const int StandartMinimumId = 1;

        public static Restaurant GetFakedRestaurant(int id)
        {
            var faker = new Faker<Restaurant>()
                .RuleFor(restaurant => restaurant.Id,
                faker => id)
                .RuleFor(restaurant => restaurant.Name,
                faker => faker.Random.Word())
                .RuleFor(restaurant => restaurant.City,
                faker => faker.Address.City())
                .RuleFor(restaurant => restaurant.Street,
                faker => faker.Address.StreetName())
                .RuleFor(restaurant => restaurant.House,
                faker => faker.Address.BuildingNumber());

            return faker.Generate();
        }

        public static Restaurant GetFakedRestaurant()
        {
            var faker = new Faker<Restaurant>()
                .RuleFor(restaurant => restaurant.Id,
                faker => faker.Random.Number(StandartMinimumId, StandartMaximumId))
                .RuleFor(restaurant => restaurant.Name,
                faker => faker.Random.Word())
                .RuleFor(restaurant => restaurant.City,
                faker => faker.Address.City())
                .RuleFor(restaurant => restaurant.Street,
                faker => faker.Address.StreetName())
                .RuleFor(restaurant => restaurant.House,
                faker => faker.Address.BuildingNumber());

            return faker.Generate();
        }

        public static Restaurant GetFakedRestaurantForInsert()
        {
            var faker = new Faker<Restaurant>()
                .RuleFor(restaurant => restaurant.Name,
                faker => faker.Random.Word())
                .RuleFor(restaurant => restaurant.City,
                faker => faker.Address.City())
                .RuleFor(restaurant => restaurant.Street,
                faker => faker.Address.StreetName())
                .RuleFor(restaurant => restaurant.House,
                faker => faker.Address.BuildingNumber());

            return faker.Generate();
        }

        public static InsertRestaurantDTO GetFakedInsertRestaurantDTO()
        {
            var faker = new Faker<InsertRestaurantDTO>()
                .RuleFor(restaurant => restaurant.Name,
                faker => faker.Random.Word())
                .RuleFor(restaurant => restaurant.City,
                faker => faker.Address.City())
                .RuleFor(restaurant => restaurant.Street,
                faker => faker.Address.StreetName())
                .RuleFor(restaurant => restaurant.House,
                faker => faker.Address.BuildingNumber());

            return faker.Generate();
        }

        public static UpdateRestaurantDTO GetFakedUpdateRestaurantDTO()
        {
            var faker = new Faker<UpdateRestaurantDTO>()
                .RuleFor(restaurant => restaurant.Name,
                faker => faker.Random.Word())
                .RuleFor(restaurant => restaurant.City,
                faker => faker.Address.City())
                .RuleFor(restaurant => restaurant.Street,
                faker => faker.Address.StreetName())
                .RuleFor(restaurant => restaurant.House,
                faker => faker.Address.BuildingNumber());

            return faker.Generate();
        }
    }
}
