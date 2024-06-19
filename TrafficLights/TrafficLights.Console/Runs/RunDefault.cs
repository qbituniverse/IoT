using Microsoft.Extensions.Logging;

namespace TrafficLights.Console.Runs;

internal class RunDefault : IRun
{
    private readonly ILogger<RunDefault> _logger;

    public RunDefault(ILogger<RunDefault> logger)
    {
        _logger = logger;
    }

    public Task Run()
    {
        _logger.LogInformation("Default Module Executed");

        Environment.Exit(-1);

        while (true)
        {
        }
        // ReSharper disable once FunctionNeverReturns
    }
}