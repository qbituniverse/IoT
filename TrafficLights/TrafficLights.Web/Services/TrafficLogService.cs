using System.Net;
using TrafficLights.Domain.Models.TrafficLog;

namespace TrafficLights.Web.Services;

public class TrafficLogService : ITrafficLogService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<TrafficLogService> _logger;

    public TrafficLogService(
        IHttpClientFactory httpClientFactory,
        ILogger<TrafficLogService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<IList<TrafficLog>> GetAllTrafficLogs()
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        _logger.LogInformation("TrafficLog Get All");
        return (await httpClient.GetFromJsonAsync<IList<TrafficLog>>("api/trafficlog/all"))!;
    }

    public async Task<TrafficLog> GetTrafficLogById(string id)
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        _logger.LogInformation($"TrafficLog Get By Id {id}");
        return (await httpClient.GetFromJsonAsync<TrafficLog>($"api/trafficlog/id/{id}"))!;
    }

    public async Task<bool> CreateDocument(TrafficLog document)
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        var response = await httpClient.PostAsJsonAsync("api/trafficlog", document);
        _logger.LogInformation("TrafficLog Create");
        return response.StatusCode == HttpStatusCode.OK;
    }

    public async Task<bool> UpdateDocument(TrafficLog document)
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        var response = await httpClient.PutAsJsonAsync($"api/trafficlog/id/{document.Id}", document);
        _logger.LogInformation($"TrafficLog Update {document.Id}");
        return response.StatusCode == HttpStatusCode.OK;
    }

    public async Task<bool> DeleteTrafficLogById(string id)
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        var response = await httpClient.DeleteAsync($"api/trafficlog/id/{id}");
        _logger.LogInformation($"TrafficLog Delete By Id {id}");
        return response.StatusCode == HttpStatusCode.OK;
    }

    public async Task<bool> DeleteDocumentsByDate(string date)
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        var response = await httpClient.DeleteAsync($"api/trafficlog/date/{date}");
        _logger.LogInformation($"TrafficLog Delete By Date {date}");
        return response.StatusCode == HttpStatusCode.OK;
    }

    public async Task<bool> DeleteAllTrafficLogs()
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        var response = await httpClient.DeleteAsync("api/trafficlog/all");
        _logger.LogInformation("TrafficLog Delete All");
        return response.StatusCode == HttpStatusCode.OK;
    }
}