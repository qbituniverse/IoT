using TrafficLights.Domain.Models.TrafficLog;

namespace TrafficLights.Web.Services;

public interface ITrafficLogService
{
    Task<IList<TrafficLog>> GetAllTrafficLogs();
    Task<TrafficLog> GetTrafficLogById(string id);
    Task<bool> CreateDocument(TrafficLog document);
    Task<bool> UpdateDocument(TrafficLog document);
    Task<bool> DeleteTrafficLogById(string id);
    Task<bool> DeleteDocumentsByDate(string date);
    Task<bool> DeleteAllTrafficLogs();
}