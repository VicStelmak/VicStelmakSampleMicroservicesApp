using Mapster;
using MapsterMapper;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using VicStelmak.Sma.ProductMicroservice.Application.Interfaces;
using VicStelmak.Sma.ProductMicroservice.Application.Services;
using VicStelmak.Sma.ProductMicroservice.Infrastructure.Consumers;
using VicStelmak.Sma.ProductMicroservice.Infrastructure.DataAccess;
using VicStelmak.Sma.ProductMicroservice.Infrastructure.DataAccess.Repositories;

namespace VicStelmak.Sma.ProductMicroservice.Api
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
            var jwtSettings = configuration.GetSection("JwtSettings");

            services.AddSingleton<ISqlDbAccess>(s => new SqlDbAccess(connectionString));
            services.AddSingleton<IProductRepository, ProductRepository>();

            services.AddAuthorization();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["secretKey"]))
                };
            });

            services.AddMassTransit(busConfiguration =>
            {
                busConfiguration.AddConsumer<OrderCreatedConsumer>((context, consumer) => 
                {
                    consumer.UseInMemoryOutbox(context);
                });
                busConfiguration.AddConsumer<OrderDeletedConsumer>((context, consumer) => 
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

