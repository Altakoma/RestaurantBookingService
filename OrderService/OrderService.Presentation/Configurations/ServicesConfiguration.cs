using MediatR;
using OrderService.Application.Interfaces.GrpcServices;
using OrderService.Application.Interfaces.Repositories.NoSql;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Application.ServicesConfigurations;
using OrderService.Application.TokenParsers;
using OrderService.Application.TokenParsers.Interfaces;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.Grpc.Services.Clients;
using OrderService.Infrastructure.Repositories.NoSql;
using OrderService.Infrastructure.Repositories.Sql;
using OrderService.Presentation.Behaviors;

namespace OrderService.Presentation.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureServices(
            this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddControllers();

            services.AddMvc(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            services.AddHttpContextAccessor();

            services.AddDatabaseContext(builder);

            services.AddHangfire(builder);

            services.AddEndpointsApiExplorer();

            services.AddSwagger();

            services.AddMapper();

            services.AddMediatR();

            services.AddFluentValidation();

            services.AddJwtTokenAuthConfiguration(builder);

            services.AddAuthorization();

            services.AddTransient(
                typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

            services.AddScoped<INoSqlOrderRepository, NoSqlOrderRepository>();

            services.AddScoped<ISqlClientRepository, SqlClientRepository>();
            services.AddScoped<ISqlMenuRepository, SqlMenuRepository>();
            services.AddScoped<ISqlOrderRepository, SqlOrderRepository>();

            services.AddSingleton<ITokenParser, JwtTokenParser>();

            services.AddGrpcClients(builder.Configuration);

            services.AddSingleton<Seed>();

            return services;
        }
    }
}
