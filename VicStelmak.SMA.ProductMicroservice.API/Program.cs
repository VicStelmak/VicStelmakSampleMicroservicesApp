using VicStelmak.Sma.ProductMicroservice.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationDependencies(builder.Configuration)
    .AddInfrastructureDependencies(builder.Configuration)
    .AddPresentationDependencies();

var app = builder.Build();

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

app.Run();
