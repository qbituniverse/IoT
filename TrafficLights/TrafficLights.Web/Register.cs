using TrafficLights.Domain.Repositories.TrafficLog;
using TrafficLights.Web.Models;
using TrafficLights.Web.Services;

namespace TrafficLights.Web;

internal class Register
{
    public static void Services(WebConfiguration configuration, IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddServerSideBlazor();

        switch (configuration.Repository!.Type)
        {
            case "MySql":
                services.AddSingleton<ITrafficLogRepository>(
                    new TrafficLogRepositoryMySql(configuration.Repository.MySql!.Url));
                break;

            case "MongoDb":
                services.AddSingleton<ITrafficLogRepository>(
                    new TrafficLogRepositoryMongoDb(configuration.Repository.MongoDb!.Url));
                break;

            default:
                services.AddSingleton<ITrafficLogRepository>(new TrafficLogRepositoryDefault());
                break;
        }

        services.AddHttpClient("TrafficLightsApi",
            httpClient => { httpClient.BaseAddress = new Uri(configuration.Api!); });
        services.AddSingleton<ITrafficControlService, TrafficControlService>();
        services.AddSingleton<ITrafficLogService, TrafficLogService>();
    }
}