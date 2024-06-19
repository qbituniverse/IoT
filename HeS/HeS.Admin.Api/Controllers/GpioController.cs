using Microsoft.AspNetCore.Mvc;
using HeS.Admin.Api.Models;

namespace HeS.Admin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GpioController : ControllerBase
{
    private readonly IGpio _gpio;
    private readonly ILogger<GpioController> _logger;

    public GpioController(IGpio gpio, ILogger<GpioController> logger)
    {
        _gpio = gpio;
        _logger = logger;
    }

    [HttpGet]
    public string GetGpio()
    {
        _logger.LogInformation("Gpio Ready");
        return "Gpio Ready";
    }

    [HttpPost]
    [Route("open")]
    public void PostGpioOpen(int pinNumber)
    {
        _logger.LogInformation("Gpio Open");
        _gpio.OpenPin(pinNumber);
        _logger.LogInformation("Done: Gpio Open");
    }

    [HttpPost]
    [Route("close")]
    public void PostGpioClose(int pinNumber)
    {
        _logger.LogInformation("Gpio Close");
        _gpio.ClosePin(pinNumber);
        _logger.LogInformation("Done: Gpio Close");
    }

    [HttpPost]
    [Route("close-all")]
    public void PostGpioCloseAll()
    {
        _logger.LogInformation("Gpio Close All");
        _gpio.CloseAllPins();
        _logger.LogInformation("Done: Gpio Close All");
    }
}