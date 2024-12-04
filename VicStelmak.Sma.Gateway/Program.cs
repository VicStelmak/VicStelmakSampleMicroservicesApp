var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenCorsPolicy", policy =>
       policy.AllowAnyOrigin()
       .AllowAnyHeader()
       .AllowAnyMethod());
});

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

app.MapReverseProxy();
app.UseCors("OpenCorsPolicy");
app.MapGet("/", () => "Hello World!");

logger.LogInformation("The gateway has started {date} at {time} Utc", DateTime.UtcNow.ToShortDateString(), DateTime.UtcNow.ToLongTimeString());

app.Run();
