using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CatalogService.Application.ServicesConfigurations
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
