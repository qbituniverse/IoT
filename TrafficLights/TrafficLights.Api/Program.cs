using TrafficLights.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddScoped<ITraffic, Traffic>();

var app = builder.Build();

app.MapControllers();

app.Run();