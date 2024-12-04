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

app.Run();
