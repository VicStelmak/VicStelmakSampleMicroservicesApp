using MassTransit;
using VicStelmak.Sma.SagaMicroservice.ApiDataLibrary;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(busConfiguration =>
{
    busConfiguration.UsingRabbitMq((context, bus) =>
    {
        bus.Host(new Uri(builder.Configuration.GetValue<string>("RabbitMqSettings:Url")), host =>
        {
            host.Username(builder.Configuration.GetValue<string>("RabbitMqSettings:Username"));
            host.Password(builder.Configuration.GetValue<string>("RabbitMqSettings:Password"));
        });

        bus.ConfigureEndpoints(context);
    });

    busConfiguration.AddSagaStateMachine<OrderSagaStateMachine, OrderSagaStateData, OrderSagaStateDataDefinition>().RedisRepository(redis => 
    {
        redis.DatabaseConfiguration(builder.Configuration.GetConnectionString("RedisSagaDbConnection"));
        redis.ConcurrencyMode = ConcurrencyMode.Optimistic;
    });
});

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

app.MapGet("/", () => "Hello World!");

logger.LogInformation("The saga microservice has started {date} at {time} Utc", DateTime.UtcNow.ToShortDateString(), 
    DateTime.UtcNow.ToLongTimeString());

app.Run();
