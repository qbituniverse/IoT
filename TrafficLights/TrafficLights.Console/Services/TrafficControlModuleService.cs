﻿using Microsoft.Extensions.Logging;
using TrafficLights.Domain.Models.TrafficLog;
using TrafficLights.Domain.Modules.TrafficControl;
using TrafficLights.Domain.Repositories.TrafficLog;

namespace TrafficLights.Console.Services;

internal class TrafficControlModuleService : ITrafficControlService
{
    private readonly ITrafficControlModule _trafficControlModule;
    private readonly ITrafficLogRepository _trafficLogRepository;
    private readonly ILogger<TrafficControlModuleService> _logger;

    public TrafficControlModuleService(
        ITrafficControlModule trafficControlModule,
        ITrafficLogRepository trafficLogRepository,
        ILogger<TrafficControlModuleService> logger)
    {
        _trafficControlModule = trafficControlModule;
        _trafficLogRepository = trafficLogRepository;
        _logger = logger;
    }

    public async Task Start()
    {
        _trafficControlModule.Start();
        await _trafficLogRepository.Create(new TrafficLog(TrafficLog.TrafficMode.Start));
        _logger.LogInformation("Traffic Start");
    }

    public async Task Stop()
    {
        _trafficControlModule.Stop();
        await _trafficLogRepository.Create(new TrafficLog(TrafficLog.TrafficMode.Stop));
        _logger.LogInformation("Traffic Stop");
    }

    public async Task Standby()
    {
        _trafficControlModule.Shut();
        _trafficControlModule.Standby();
        await _trafficLogRepository.Create(new TrafficLog(TrafficLog.TrafficMode.Standby));
        _logger.LogInformation("Traffic Standby");
    }

    public async Task Shut()
    {
        _trafficControlModule.Shut();
        await _trafficLogRepository.Create(new TrafficLog(TrafficLog.TrafficMode.Shut));
        _logger.LogInformation("Traffic Shut");
    }

    public async Task Test(int blinkTime, int pinNumber)
    {
        _trafficControlModule.Shut();
        _trafficControlModule.Test(blinkTime, pinNumber);
        await _trafficLogRepository.Create(new TrafficLog(TrafficLog.TrafficMode.Test));
        _logger.LogInformation($"Traffic Test BlinkTime {blinkTime} PinNumber {pinNumber}");
    }
}