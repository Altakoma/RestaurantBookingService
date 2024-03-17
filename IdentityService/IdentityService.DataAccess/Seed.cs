using IdentityService.DataAccess.DatabaseContext;
using IdentityService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.DataAccess
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
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

                dbContext.Database.Migrate();

                if (!dbContext.UserRoles.Any())
                {
                    SeedUserRolesAndUsers(dbContext);
                }

                dbContext.SaveChanges();
            }
        }

        private void SeedUserRolesAndUsers(IdentityDbContext dbContext)
        {
            var roles = new List<UserRole>
            {
                new UserRole
                {
                    Name = "Admin",
                    Users = new List<User>
                    {
                        new User()
                        {
                            Name = "maksim",
                            Login = "maksim",
                            Password = "miskam",
                        }
                    }
                },
                new UserRole
                {
                    Name = "Client",
                    Users =  new List<User>
                    {
                        new User()
                        {
                            Name = "andrei",
                            Login = "andrei",
                            Password = "ierdna",
                        },
                        new User()
                        {
                            Name = "artur",
                            Login = "artur",
                            Password = "rutra",
                        }
                    }
                }
            };

            dbContext.UserRoles.AddRange(roles);
        }
    }
}
