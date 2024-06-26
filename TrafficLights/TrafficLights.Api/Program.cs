using MongoDB.Bson;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using TrafficLights.Api;
using TrafficLights.Api.Models;
using RollingInterval = Serilog.Sinks.MongoDB.RollingInterval;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiConfiguration>(
    builder.Configuration.GetSection(nameof(ApiConfiguration))
);

var apiConfiguration = builder.Configuration.GetSection(nameof(ApiConfiguration)).Get<ApiConfiguration>();

var loggerConfiguration = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .Enrich.WithProcessId()
    .Enrich.WithProcessName()
    .Enrich.WithEnvironmentName()
    .Enrich.WithEnvironmentUserName()
    .WriteTo.Console(
        restrictedToMinimumLevel: LogEventLevel.Information,
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}",
        theme: AnsiConsoleTheme.Code);

switch (apiConfiguration!.Repository!.Type)
{
    case "MySql":
        loggerConfiguration.WriteTo.MySQL(
            connectionString: apiConfiguration.Repository.MySql!.Url, 
            tableName: "ApiLogs",
            restrictedToMinimumLevel: LogEventLevel.Warning);
        break;
        
    case "MongoDb":
        loggerConfiguration.WriteTo.MongoDBBson(
            restrictedToMinimumLevel: LogEventLevel.Warning,
            databaseUrl: $"{apiConfiguration.Repository.MongoDb!.Url}/TrafficLights",
            collectionName: "ApiLogs",
            rollingInterval: RollingInterval.Day,
            cappedMaxSizeMb: 1024,
            cappedMaxDocuments: 50000);
        break;
}

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(loggerConfiguration.CreateLogger());

Register.Services(apiConfiguration, builder.Services);

var app = builder.Build();

app.Logger.LogInformation(@"TrafficLights.Api Configuration {Config}", apiConfiguration.ToJson());

app.UseExceptionHandler("/error");

app.MapControllers();

app.Run();