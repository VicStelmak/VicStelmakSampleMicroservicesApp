using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Features.Order;
using VicStelmak.Sma.OrderMicroservice.ApiDataLibrary.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.ConfigureOrderEndpoints();

logger.LogInformation("The order microservice has started {date} at {time} Utc", DateTime.UtcNow.ToShortDateString(), 
    DateTime.UtcNow.ToLongTimeString());

app.Run();