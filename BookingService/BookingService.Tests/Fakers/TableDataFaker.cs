using Bogus;
using BookingService.Domain.Entities;

namespace BookingService.Tests.Fakers
{
    public class TableDataFaker
    {
        public static Table GetFakedTable()
        {
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();

            var faker = new Faker<Table>()
                .RuleFor(table => table.Id,
                faker => faker.Random.Number(min: 1, max: 20))
                .RuleFor(table => table.SeatsCount,
                faker => faker.Random.Number(min: 1, max: 20));

            Table table = faker.Generate();

            table.Restaurant = restaurant;
            table.RestaurantId = restaurant.Id;

            return table;
        }

        public static Table GetFakedTableForInsert(int restaurantId)
        {
            var faker = new Faker<Table>()
                .RuleFor(table => table.RestaurantId,
                faker => restaurantId)
                .RuleFor(table => table.SeatsCount,
                faker => faker.Random.Number(min: 1, max: 20));

            return faker.Generate();
        }
    }
}
