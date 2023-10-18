using FluentValidation;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.BusinessLogic.Services;
using IdentityService.BusinessLogic.Services.Interfaces;
using IdentityService.BusinessLogic.ServicesConfigurations;
using IdentityService.BusinessLogic.TokenGenerators;
using IdentityService.DataAccess;
using IdentityService.DataAccess.Repositories;
using IdentityService.DataAccess.Repositories.Interfaces;

namespace IdentityService.API.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            services.AddControllers();

            services.AddMvc(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            services.AddDatabaseContext(builder);

            services.AddHttpContextAccessor();

            services.AddEndpointsApiExplorer();

            services.AddSwagger();

            services.AddFluentValidation();

            services.AddJwtTokenAuthConfiguration(builder);

            services.AddAuthorization();

            services.AddSingleton<Seed>();

            services.AddMapper();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            services.AddSingleton<ITokenGenerator, JwtGenerator>();

            return services;
        }
    }
}
