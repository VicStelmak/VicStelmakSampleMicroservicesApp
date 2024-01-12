using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VicStelmak.SMA.ProductMicroservice.Infrastructure
{
    public static class DIContainersConfigurator
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresProductDbConnection") ??
                throw new InvalidOperationException("Connection string 'PostgresProductDbConnection' not found.");

            return services;
        }
    }
}
