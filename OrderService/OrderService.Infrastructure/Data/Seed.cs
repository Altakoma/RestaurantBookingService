using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data.ApplicationSQLDbContext;

namespace OrderService.Infrastructure.Data
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
                var dbContext = scope.ServiceProvider.GetRequiredService<OrderServiceSqlDbContext>();

                dbContext.Database.Migrate();

                SaveGeneratedData(dbContext);
            }
        }

        private void SaveGeneratedData(OrderServiceSqlDbContext dbContext)
        {
            if (!dbContext.Menu.Any())
            {
                ICollection<Menu> menu = GenerateMenu();
                dbContext.Menu.AddRange(menu);
            }

            if (!dbContext.Clients.Any())
            {
                ICollection<Client> clients = GenerateClients();
                dbContext.Clients.AddRange(clients);
            }

            dbContext.SaveChanges();
        }

        private ICollection<Menu> GenerateMenu()
        {
            var menu = new List<Menu>
            {
                new Menu
                {
                    Id = 1,
                    FoodName = "Milk",
                    Cost = 1,
                },
                new Menu
                {
                    Id = 2,
                    FoodName = "Cookies",
                    Cost = 5,
                },
                new Menu
                {
                    Id = 3,
                    FoodName = "Chocolate",
                    Cost = 3,
                },
            };

            return menu;
        }

        private ICollection<Client> GenerateClients()
        {
            var clients = new List<Client>
            {
                new Client
                {
                    Id = 1,
                    Name = "maksim",
                },
                new Client
                {
                    Id = 2,
                    Name = "andrei",
                },
                new Client
                {
                    Id = 3,
                    Name = "artur",
                }
            };

            return clients;
        }
    }
}
