using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VicStelmak.SMA.ProductMicroservice.Infrastructure
{
    public static class DIContainersConfigurator
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
           

            return services;
        }
    }
}
