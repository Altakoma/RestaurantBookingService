using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CatalogService.Application.ServicesConfigurations
{
    public static class FluentValidationConfiguration
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
