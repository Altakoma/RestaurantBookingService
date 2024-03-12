using Bogus;
using OrderService.Application.DTOs.Order;
using OrderService.Application.MediatR.Order.Queries;
using OrderService.Domain.Entities;

namespace OrderService.Tests.Fakers
{
    public static class OrderDataFaker
    {
        public static Order GetFakedOrder()
        {
            var faker = new Faker<Order>()
                .RuleFor(query => query.Id,
                faker => faker.Random.Number(min: 1, max: 20))
                .RuleFor(query => query.BookingId,
                faker => faker.Random.Number(min: 1, max: 20));

            var order = faker.Generate();

            order.Menu = MenuDataFaker.GetFakedMenu();
            order.MenuId = order.Menu.Id;
            order.Client = ClientDataFaker.GetFakedClient();
            order.ClientId = order.Client.Id;

            return order;
        }

        public static GetAllOrdersQuery GetAllOrdersFakedQuery()
        {
            var faker = new Faker<GetAllOrdersQuery>()
                .RuleFor(query => query.SkipCount,
                faker => faker.Random.Number(min: 1, max: 20))
                .RuleFor(query => query.SelectionAmount,
                faker => faker.Random.Number(min: 1, max: 20));

            return faker.Generate();
        }

        public static GetOrderByIdQuery GetOrderByIdFakedQuery()
        {
            var faker = new Faker<GetOrderByIdQuery>()
                .RuleFor(query => query.Id,
                faker => faker.Random.Number(min: 1, max: 20));

            return faker.Generate();
        }

        public static ReadOrderDTO GetFakedReadOrderDTO()
        {
            var faker = new Faker<ReadOrderDTO>()
                .RuleFor(query => query.Id,
                faker => faker.Random.Number(min: 1, max: 20))
                .RuleFor(query => query.BookingId,
                faker => faker.Random.Number(min: 1, max: 20));

            var readOrderDTO = faker.Generate();

            readOrderDTO.ReadMenuDTO = MenuDataFaker.GetFakedReadMenuDTO();
            readOrderDTO.ReadClientDTO = ClientDataFaker.GetFakedReadClientDTO();

            return readOrderDTO;
        }

        public static ReadOrderDTO GetFakedReadOrderDTO(int bookingId, int menuId)
        {
            var faker = new Faker<ReadOrderDTO>()
                .RuleFor(query => query.Id,
                faker => faker.Random.Number(min: 1, max: 20))
                .RuleFor(query => query.BookingId,
                faker => bookingId);

            var readOrderDTO = faker.Generate();

            readOrderDTO.ReadMenuDTO = MenuDataFaker.GetFakedReadMenuDTO(menuId);
            readOrderDTO.ReadClientDTO = ClientDataFaker.GetFakedReadClientDTO();

            return readOrderDTO;
        }

        public static ReadOrderDTO GetFakedReadOrderDTO(int menuId)
        {
            var faker = new Faker<ReadOrderDTO>()
                .RuleFor(query => query.Id,
                faker => faker.Random.Number(min: 1, max: 20))
                .RuleFor(query => query.BookingId,
                faker => faker.Random.Number(min: 1, max: 20));

            var readOrderDTO = faker.Generate();

            readOrderDTO.ReadMenuDTO = MenuDataFaker.GetFakedReadMenuDTO(menuId);
            readOrderDTO.ReadClientDTO = ClientDataFaker.GetFakedReadClientDTO();

            return readOrderDTO;
        }

        public static InsertOrderDTO GetFakedInsertOrderDTO()
        {
            var faker = new Faker<InsertOrderDTO>()
                .RuleFor(query => query.BookingId,
                faker => faker.Random.Number(min: 1, max: 20))
                .RuleFor(query => query.MenuId,
                faker => faker.Random.Number(min: 1, max: 20));

            return faker.Generate();
        }

        public static UpdateOrderDTO GetFakedUpdateOrderDTO()
        {
            var faker = new Faker<UpdateOrderDTO>()
                .RuleFor(query => query.MenuId,
                faker => faker.Random.Number(min: 1, max: 20));

            return faker.Generate();
        }
    }
}
