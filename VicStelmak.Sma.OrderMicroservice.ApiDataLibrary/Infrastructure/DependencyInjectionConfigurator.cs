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
            const string RabbitMqUrl = "rabbitmq://localhost/";
            const string UserName = "guest";
            const string Password = "guest";

            var connectionString = configuration.GetConnectionString("PostgresOrderDbConnection") ??
                throw new InvalidOperationException("Connection string 'PostgresOrderDbConnection' not found.");
            
            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            
            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<MediatREntrypoint>());

            services.AddSingleton<ISqlDbAccess>(s => new SqlDbAccess(connectionString));
            services.AddSingleton<IOrderRepository, OrderRepository>();

            services.AddMassTransit(configuration =>
            {
                configuration.AddConsumer<OrderCreatingConsumer>((context, configurator) => 
                {
                    configurator.UseInMemoryOutbox(context);
                });
                configuration.UsingRabbitMq((context, bus) =>
                {
                    bus.Host(new Uri(RabbitMqUrl), host =>
                    {
                        host.Username(UserName);
                        host.Password(Password);
                    });

                    bus.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
