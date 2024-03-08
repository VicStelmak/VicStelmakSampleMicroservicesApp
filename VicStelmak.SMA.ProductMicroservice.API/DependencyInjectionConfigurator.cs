using Mapster;
using MapsterMapper;
using System.Reflection;
using VicStelmak.SMA.ProductMicroservice.Application.Interfaces;
using VicStelmak.SMA.ProductMicroservice.Application.Services;
using VicStelmak.SMA.ProductMicroservice.Infrastructure.DataAccess;
using VicStelmak.SMA.ProductMicroservice.Infrastructure.DataAccess.Repositories;

namespace VicStelmak.SMA.ProductMicroservice.Api
{
    public static class DependencyInjectionConfigurator
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var mapperConfiguration = TypeAdapterConfig.GlobalSettings;
            mapperConfiguration.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton(mapperConfiguration);
            services.AddSingleton<IMapper, ServiceMapper>();

            return services;
        }

        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresProductDbConnection") ??
                throw new InvalidOperationException("Connection string 'PostgresProductDbConnection' not found.");

            services.AddSingleton<ISqlDbAccess>(s => new SqlDbAccess(connectionString));
            services.AddSingleton<IProductRepository, ProductRepository>();

            return services;
        }

        public static IServiceCollection AddPresentationDependencies(this IServiceCollection services)
        {
            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}

