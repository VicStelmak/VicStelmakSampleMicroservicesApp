using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VicStelmak.SMA.ProductMicroservice.Application.Interfaces;
using VicStelmak.SMA.ProductMicroservice.Infrastructure.DataAccess;
using VicStelmak.SMA.ProductMicroservice.Infrastructure.DataAccess.Repositories;

namespace VicStelmak.SMA.ProductMicroservice.Infrastructure
{
    public static class DIContainersConfigurator
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresProductDbConnection") ?? 
                throw new InvalidOperationException("Connection string 'PostgresProductDbConnection' not found.");

            services.AddSingleton<ISqlDbAccess>(s => new SqlDbAccess(connectionString));
            services.AddSingleton<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
