using IdentityService.BusinessLogic.KafkaMessageBroker.Interfaces.Producers;
using IdentityService.BusinessLogic.KafkaMessageBroker.Producers;
using IdentityService.BusinessLogic.Services;
using IdentityService.BusinessLogic.Services.Interfaces;
using IdentityService.BusinessLogic.ServicesConfigurations;
using IdentityService.BusinessLogic.TokenGenerators;
using IdentityService.DataAccess;
using IdentityService.DataAccess.CacheAccess;
using IdentityService.DataAccess.CacheAccess.Interfaces;
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

            services.ConfigureRedis(builder.Configuration);

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddGrpc();

            services.AddDatabaseContext(builder);

            services.AddHttpContextAccessor();

            services.AddEndpointsApiExplorer();

            services.AddSwagger();

            services.ConfigureKafkaOptions(builder.Configuration);

            services.AddFluentValidation();

            services.AddJwtTokenAuthConfiguration(builder);

            services.AddAuthorization();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", corsPolicyBuilder =>
                corsPolicyBuilder.SetIsOriginAllowed(origin =>
                    new Uri(origin).Host == (builder.Configuration["CorsPolicyHost"]))
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });

            services.AddSingleton<Seed>();

            services.AddMapper();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();

            services.AddScoped<IRefreshTokenCacheAccessor, RefreshTokenCacheAccessor>();

            services.AddScoped<ICookieService, CookieService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            services.AddSingleton<ITokenGenerator, JwtGenerator>();
            services.AddSingleton<IUserMessageProducer, UserMessageProducer>();

            return services;
        }
    }
}
