using Bogus;
using BookingService.Domain.Entities;

namespace BookingService.Tests.Fakers
{
    public class ClientDataFaker
    {
        public static Client GetFakedClient()
        {
            var faker = new Faker<Client>()
                .RuleFor(client => client.Id,
                faker => faker.Random.Number(min: 1, max: 20))
                .RuleFor(client => client.Name,
                faker => faker.Random.Word());

            return faker.Generate();
        }
    }
}
