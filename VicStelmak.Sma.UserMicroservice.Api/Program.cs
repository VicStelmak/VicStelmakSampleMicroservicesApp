using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Application.Interfaces;
using VicStelmak.Sma.UserMicroservice.ApiDataLibrary.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("OpenCorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

logger.LogInformation("The user microservice has started {date} at {time} Utc", DateTime.UtcNow.ToShortDateString(), 
    DateTime.UtcNow.ToLongTimeString());

using (var serviceScope = app.Services.CreateScope())
{
    var userService = serviceScope.ServiceProvider.GetRequiredService<IUserService>();

    await userService.AddRoleToUserAsync("Administrator", "7f335875-a73d-3773-a7c7-937a53fd7330");
}

app.Run();
