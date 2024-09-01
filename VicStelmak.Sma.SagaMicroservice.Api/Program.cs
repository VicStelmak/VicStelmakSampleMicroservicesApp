using MassTransit;
using VicStelmak.Sma.SagaMicroservice.ApiDataLibrary;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(configuration =>
{
    const string Password = "guest";
    const string RabbitMqUrl = "rabbitmq://localhost/";
    const string RedisConnectionString = "127.0.0.1";
    const string UserName = "guest";

    configuration.UsingRabbitMq((context, bus) =>
    {
        bus.Host(new Uri(RabbitMqUrl), host =>
        {
            host.Username(UserName);
            host.Password(Password);
        });

        bus.ConfigureEndpoints(context);
    });

    configuration.AddSagaStateMachine<OrderSagaStateMachine, OrderSagaStateData, OrderSagaStateDataDefinition>().RedisRepository(configuration => 
    {
        configuration.DatabaseConfiguration(RedisConnectionString);
        configuration.ConcurrencyMode = ConcurrencyMode.Optimistic;
    });
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
