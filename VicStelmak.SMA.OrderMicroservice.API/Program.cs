using VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Features.DeliveryAddress;
using VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Features.Order;
using VicStelmak.Sma.OrderMicroservice.APIDataLibrary.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureOrderEndpoints();
app.ConfigureDeliveryAddressEndpoints();

app.Run();