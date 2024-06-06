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

app.MapReverseProxy();
app.UseCors("OpenCorsPolicy");
app.MapGet("/", () => "Hello World!");

app.Run();
