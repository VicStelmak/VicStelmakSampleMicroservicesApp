using VicStelmak.Sma.ProductMicroservice.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationDependencies(builder.Configuration)
    .AddInfrastructureDependencies(builder.Configuration)
    .AddPresentationDependencies();

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.ConfigureApi();

logger.LogInformation("The product microservice has started {date} at {time} Utc", DateTime.UtcNow.ToShortDateString(), 
    DateTime.UtcNow.ToLongTimeString());

app.Run();