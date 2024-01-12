using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VicStelmak.SMA.OrderMicroservice.Infrastructure
{
    public static class DIContainersConfigurator
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresOrderDbConnection") ??
                throw new InvalidOperationException("Connection string 'PostgresOrderDbConnection' not found.");

            return services;
        }
    }
}
