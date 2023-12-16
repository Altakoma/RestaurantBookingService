using Bogus;
using BookingService.Domain.Entities;
using FluentAssertions.Extensions;

namespace BookingService.Tests.Fakers
{
    public static class BookingDataFaker
    {
        public static Booking GetFakedBooking()
        {
            Table table = TableDataFaker.GetFakedTable();
            Client client = ClientDataFaker.GetFakedClient();

            var faker = new Faker<Booking>()
                .RuleFor(booking => booking.Id,
                faker => faker.Random.Number(min: 1, max: 20))
                .RuleFor(booking => booking.BookingTime,
                faker => faker.Date.Recent());

            Booking booking = faker.Generate();
            booking.Client = client;
            booking.Table = table;
            booking.ClientId = client.Id;
            booking.TableId = table.Id;

            return booking;
        }

        public static Booking GetFakedBookingForInsert()
        {
            Table table = TableDataFaker.GetFakedTableForInsert();
            Client client = ClientDataFaker.GetFakedClient();

            var faker = new Faker<Booking>()
                .RuleFor(booking => booking.Table,
                faker => table)
                .RuleFor(booking => booking.Client,
                faker => client)
                .RuleFor(booking => booking.BookingTime,
                faker => faker.Date.Recent().AsUtc());

            return faker.Generate();
        }
    }
}
