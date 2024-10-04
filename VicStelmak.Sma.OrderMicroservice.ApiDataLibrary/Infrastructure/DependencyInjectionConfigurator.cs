using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Infrastructure.DataAccess;

namespace VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Infrastructure
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
            services.AddSingleton<IOrderRepository, OrderRepository>();

            services.AddMassTransit(busConfiguration =>
            {
                busConfiguration.AddConsumer<OrderCreatingConsumer>((context, consumer) => 
                {
                    consumer.UseInMemoryOutbox(context);
                });
                busConfiguration.UsingRabbitMq((context, bus) =>
                {
                    bus.Host(new Uri(configuration.GetValue<string>("RabbitMqSettings:Url")), host =>
                    {
                        host.Username(configuration.GetValue<string>("RabbitMqSettings:Username"));
                        host.Password(configuration.GetValue<string>("RabbitMqSettings:Password"));
                    });

                    bus.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
