using BookingService.Domain.Entities;
using BookingService.Infrastructure.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Infrastructure.Data
{
    public class Seed
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Seed(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void SeedData()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookingServiceDbContext>();

                dbContext.Database.Migrate();

                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = new List<Restaurant>
                    {
                        new Restaurant()
                        {
                            Id = 1,
                            Name = "DeliciousFood",
                            Tables = new List<Table>
                            {
                                new Table
                                {
                                    SeatsCount = 4,
                                },
                                new Table
                                {
                                    SeatsCount = 2,
                                },
                                new Table
                                {
                                    SeatsCount = 5,
                                },
                            }
                        },
                        new Restaurant()
                        {
                            Id = 2,
                            Name = "SpicyFood",
                            Tables = new List<Table>
                            {
                                new Table
                                {
                                    SeatsCount = 2,
                                },
                                new Table
                                {
                                    SeatsCount = 2,
                                },
                                new Table
                                {
                                    SeatsCount = 2,
                                },
                            }
                        },
                        new Restaurant()
                        {
                            Id = 3,
                            Name = "Menu for you",
                            Tables = new List<Table>
                            {
                                new Table
                                {
                                    SeatsCount = 6,
                                },
                                new Table
                                {
                                    SeatsCount = 5,
                                },
                                new Table
                                {
                                    SeatsCount = 3,
                                },
                            }
                        },
                    };

                    dbContext.Restaurants.AddRange(restaurants);
                }

                if (!dbContext.Clients.Any())
                {
                    var clients = new List<Client>
                    {
                        new Client
                        {
                            Id = 1,
                            Name = "client1",
                        },
                        new Client
                        {
                            Id = 2,
                            Name = "client2",
                        },
                        new Client
                        {
                            Id = 3,
                            Name = "client3",
                        }
                    };

                    dbContext.Clients.AddRange(clients);
                }

                dbContext.SaveChanges();

                if (!dbContext.Bookings.Any())
                {
                    var bookings = new List<Booking>
                    {
                        new Booking
                        {
                            ClientId = 1,
                            TableId = 3,
                            BookingTime = DateTime.UtcNow +TimeSpan.FromDays(30),
                        },
                        new Booking
                        {
                            ClientId = 2,
                            TableId = 4,
                            BookingTime = DateTime.UtcNow + TimeSpan.FromDays(30),
                        },
                        new Booking
                        {
                            ClientId = 3,
                            TableId = 5,
                            BookingTime = DateTime.UtcNow + TimeSpan.FromDays(30),
                        },
                    };

                    dbContext.Bookings.AddRange(bookings);
                }

                dbContext.SaveChanges();
            }
        }
    }
}
