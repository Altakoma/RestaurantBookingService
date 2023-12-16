using Bogus;
using BookingService.Domain.Entities;

namespace BookingService.Tests.Fakers
{
    public class RestaurantDataFaker
    {
        public static Restaurant GetFakedRestaurant()
        {
            var faker = new Faker<Restaurant>()
                .RuleFor(table => table.Id,
                faker => faker.Random.Number(min: 1, max: 20))
                .RuleFor(table => table.Name,
                faker => faker.Random.Word());

            return faker.Generate();
        }

        public static Restaurant GetFakedRestaurantForInsert()
        {
            var faker = new Faker<Restaurant>()
                .RuleFor(table => table.Name,
                faker => faker.Random.Word());

            return faker.Generate();
        }
    }
}
