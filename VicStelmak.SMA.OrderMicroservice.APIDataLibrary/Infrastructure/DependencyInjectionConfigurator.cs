using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Features.DeliveryAddress;
using VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Features.Order;
using VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Infrastructure.DataAccess;

namespace VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Infrastructure
{
    public static class DependencyInjectionConfigurator
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresOrderDbConnection") ??
                throw new InvalidOperationException("Connection string 'PostgresOrderDbConnection' not found.");
            
            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            
            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<MediatREntrypoint>());

            services.AddSingleton<ISqlDbAccess>(s => new SqlDbAccess(connectionString));
            services.AddSingleton<IDeliveryAddressRepository, DeliveryAddressRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
