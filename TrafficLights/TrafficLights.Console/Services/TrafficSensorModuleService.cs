using Microsoft.Extensions.Logging;
using TrafficLights.Domain.Models.TrafficSensor;
using TrafficLights.Domain.Modules.TrafficSensor;

namespace TrafficLights.Console.Services;

internal class TrafficSensorModuleService : ITrafficSensorService
{
    private readonly ITrafficSensorModule _trafficSensorModule;
    private readonly ILogger<TrafficSensorModuleService> _logger;

    public event EventHandler<TrafficSensor>? SensorValueChangedEvent;

    public TrafficSensorModuleService(
        ITrafficSensorModule trafficSensorModule,
        ILogger<TrafficSensorModuleService> logger)
    {
        _trafficSensorModule = trafficSensorModule;
        _logger = logger;
    }

    public void Invoke()
    {
        _trafficSensorModule.SensorValueChangedEvent += SensorValueChangedEvent;
        _trafficSensorModule.Invoke();
        _logger.LogInformation("Sensor Invoked");
    }
}