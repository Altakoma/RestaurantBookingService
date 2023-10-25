using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure.Data
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
                var dbContext = scope.ServiceProvider.GetRequiredService<CatalogServiceDbContext>();

                dbContext.Database.Migrate();

                SaveGeneratedData(dbContext);
            }
        }

        private void SaveGeneratedData(CatalogServiceDbContext dbContext)
        {
            if (!dbContext.Restaurants.Any())
            {
                ICollection<Menu> menu = GenerateMenu();

                ICollection<Restaurant> restaurants = GenerateRestaurants(menu);

                dbContext.Restaurants.AddRange(restaurants);
            }

            dbContext.SaveChanges();
        }

        private ICollection<Menu> GenerateMenu()
        {
            var milkFoodType = new FoodType()
            {
                Name = "Milk product",
            };

            var chocolateFoodType = new FoodType()
            {
                Name = "Chocolate product",
            };

            var bakeFoodType = new FoodType()
            {
                Name = "Bake product",
            };

            var menu = new List<Menu>
            {
                new Menu
                {
                    FoodName = "Milk",
                    Cost = 1,
                    FoodType = milkFoodType,
                },
                new Menu
                {
                    FoodName = "Cookies",
                    Cost = 5,
                    FoodType = bakeFoodType,
                },
                new Menu
                {
                    FoodName = "Chocolate",
                    Cost = 3,
                    FoodType = chocolateFoodType,
                },
            };

            return menu;
        }

        private ICollection<Restaurant> GenerateRestaurants(ICollection<Menu> menu)
        {
            var restaurants = new List<Restaurant>
            {
                new Restaurant()
                {
                    Name = "DeliciousFood",
                    City = "London",
                    Street = "Oxford",
                    House = "25",
                    Employees = new List<Employee>
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "andrei",
                        },
                        new Employee
                        {
                            Id = 2,
                            Name = "arthur",
                        },
                        new Employee
                        {
                            Id = 3,
                            Name = "maksim",
                        }
                    },
                    Menu = menu,
                },
                new Restaurant()
                {
                    Name = "SpicyFood",
                    City = "London",
                    Street = "Oxford",
                    House = "26",
                    Employees = new List<Employee>
                    {
                        new Employee
                        {
                            Id = 4,
                            Name = "dmitriy",
                        },
                        new Employee
                        {
                            Id = 5,
                            Name = "nikolai",
                        },
                        new Employee
                        {
                            Id = 6,
                            Name = "yaroslav",
                        }
                    },
                    Menu = menu,
                },
                new Restaurant()
                {
                    Name = "Menu for you",
                    City = "London",
                    Street = "Oxford",
                    House = "27",
                    Employees = new List<Employee>
                    {
                        new Employee
                        {
                            Id = 7,
                            Name = "ilya",
                        },
                        new Employee
                        {
                            Id = 8,
                            Name = "anya",
                        },
                        new Employee
                        {
                            Id = 9,
                            Name = "gleb",
                        }
                    },
                    Menu = menu,
                },
            };

            return restaurants;
        }
    }
}
