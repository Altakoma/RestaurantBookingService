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
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

                dbContext.Database.Migrate();

                if (!dbContext.UserRoles.Any())
                {
                    SeedUserRoles(dbContext);
                }

                if (!dbContext.Users.Any())
                {
                    SeedUsers(dbContext);
                }

                dbContext.SaveChanges();
            }
        }

        private void SeedUserRoles(IdentityDbContext dbContext)
        {
            var roles = new List<UserRole>
            {
                new UserRole
                {
                    Name = "Admin",
                },
                new UserRole
                {
                    Name = "Client",
                }
            };

            dbContext.UserRoles.AddRange(roles);
        }

        private void SeedUsers(IdentityDbContext dbContext)
        {
            var users = new List<User>
            {
                new User()
                {
                    Name = "andrei",
                    Login = "andrei",
                    Password = "ierdna",
                    UserRoleId = 1,
                },
                new User()
                {
                    Name = "maksim",
                    Login = "maksim",
                    Password = "miskam",
                    UserRoleId = 2,
                },
                new User()
                {
                    Name = "artur",
                    Login = "artur",
                    Password = "rutra",
                    UserRoleId = 1,
                }
            };

            dbContext.Users.AddRange(users);
        }
    }
}
