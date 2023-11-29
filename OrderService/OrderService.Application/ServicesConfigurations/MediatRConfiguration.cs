using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.ServicesConfigurations;
using System.Reflection;

namespace OrderService.Application.ServicesConfigurations
{
    public static class MediatRConfiguration
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
