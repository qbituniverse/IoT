using HeS.Admin.Api.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddScoped<IGpio, Gpio>();

var app = builder.Build();

app.MapControllers();

app.Run();