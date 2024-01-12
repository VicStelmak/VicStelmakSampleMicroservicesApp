using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VicStelmak.SMA.CustomerMicroservice.Infrastructure
{
    public static class DIContainersConfigurator
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresCustomerDbConnection") ??
                throw new InvalidOperationException("Connection string 'PostgresCustomerDbConnection' not found.");

            return services;
        }
    }
}
