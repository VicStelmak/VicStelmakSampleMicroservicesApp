using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VicStelmak.SMA.OrderMicroservice.Infrastructure
{
    public static class DIContainersConfigurator
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            

            return services;
        }
    }
}
