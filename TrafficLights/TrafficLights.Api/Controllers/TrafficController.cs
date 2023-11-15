using Microsoft.AspNetCore.Mvc;
using TrafficLights.Models;

namespace TrafficLights.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrafficController : ControllerBase
{
    private readonly ITraffic _traffic;
    private readonly ILogger<TrafficController> _logger;

    public TrafficController(ITraffic traffic, ILogger<TrafficController> logger)
    {
        _traffic = traffic;
        _logger = logger;
    }

    [HttpGet]
    public string GetTraffic()
    {
        _logger.LogInformation("Traffic Ready");
        return "Traffic Ready";
    }

    [HttpPost]
    [Route("start")]
    public void PostTrafficStart()
    {
        _logger.LogInformation("Traffic Start");
        _traffic.Start();
        _logger.LogInformation("Done: Traffic Start");
    }

    [HttpPost]
    [Route("stop")]
    public void PostTrafficStop()
    {
        _logger.LogInformation("Traffic Stop");
        _traffic.Stop();
        _logger.LogInformation("Done: Traffic Stop");
    }

    [HttpPost]
    [Route("standby")]
    public void PostTrafficStandby()
    {
        _logger.LogInformation("Traffic Standby");
        _traffic.Shut();
        _traffic.Stanby();
        _logger.LogInformation("Done: Traffic Standby");
    }

    [HttpPost]
    [Route("shut")]
    public void PostTrafficShut()
    {
        _logger.LogInformation("Traffic Shut");
        _traffic.Shut();
        _logger.LogInformation("Done: Traffic Shut");
    }

    [HttpPost]
    [Route("test")]
    public void PostTrafficTest(int blinkTime, int pinNumber)
    {
        _logger.LogInformation("Traffic Test");
        _traffic.Shut();
        _traffic.Test(blinkTime, pinNumber);
        _logger.LogInformation("Done: Traffic Test");
    }
}