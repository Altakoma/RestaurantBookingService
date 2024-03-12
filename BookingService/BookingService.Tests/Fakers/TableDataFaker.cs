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
    }
}
