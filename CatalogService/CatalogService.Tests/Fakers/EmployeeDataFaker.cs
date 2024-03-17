using Bogus;
using CatalogService.Domain.Entities;

namespace CatalogService.Tests.Fakers
{
    internal static class EmployeeDataFaker
    {
        public const int StandartMaximumId = 15;
        public const int StandartMinimumId = 1;

        internal static Employee GetFakedEmployee()
        {
            var faker = new Faker<Employee>()
                .RuleFor(employee => employee.Id,
                faker => faker.Random.Number(StandartMinimumId, StandartMaximumId))
                .RuleFor(employee => employee.RestaurantId,
                faker => faker.Random.Number(StandartMinimumId, StandartMaximumId))
                .RuleFor(employee => employee.Name,
                faker => faker.Random.Word());

            return faker.Generate();
        }
    }
}
