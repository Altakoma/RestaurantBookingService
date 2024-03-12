using Bogus;
using OrderService.Application.DTOs.Client;
using OrderService.Domain.Entities;

namespace OrderService.Tests.Fakers
{
    internal static class ClientDataFaker
    {
        internal static Client GetFakedClient()
        {
            var faker = new Faker<Client>()
            .RuleFor(readClientDTO => readClientDTO.Id,
                faker => faker.Random.Number(min: 1, max: 20))
            .RuleFor(readClientDTO => readClientDTO.Name,
                faker => faker.Random.Word());

            return faker.Generate();
        }

        internal static ReadClientDTO GetFakedReadClientDTO()
        {
            var faker = new Faker<ReadClientDTO>()
            .RuleFor(readClientDTO => readClientDTO.Id,
                faker => faker.Random.Number(min: 1, max: 20))
            .RuleFor(readClientDTO => readClientDTO.Name,
                faker => faker.Random.Word());

            return faker.Generate();
        }
    }
}
