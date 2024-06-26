﻿using System.Text;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TrafficLights.Api.Models;

namespace TrafficLights.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly ApiConfiguration _apiConfiguration;
    private readonly ILogger<AdminController> _logger;

    public AdminController(
        IOptions<ApiConfiguration> apiConfiguration, 
        ILogger<AdminController> logger)
    {
        _apiConfiguration = apiConfiguration.Value;
        _logger = logger;
    }

    [HttpGet]
    [Route("ping")]
    public IActionResult GetPing()
    {
        _logger.LogInformation("Ping");
        return Ok("Ping");
    }

    [HttpGet]
    [Route("config")]
    public IActionResult GetConfiguration()
    {
        var config = new StringBuilder();
        config.AppendLine($"Environment: {_apiConfiguration.Environment}");
        config.AppendLine($"Repository.Type: {_apiConfiguration.Repository!.Type}");
        config.AppendLine($"Repository.MySql.Url: {_apiConfiguration.Repository.MySql!.Url}");
        config.AppendLine($"Repository.MongoDb.Url: {_apiConfiguration.Repository.MongoDb!.Url}");
        config.AppendLine($"TrafficControl.Type: {_apiConfiguration.TrafficControl!.Type}");
        _logger.LogInformation(config.ToString());
        return Ok(config.ToString());
    }

    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleError([FromServices] IHostEnvironment hostEnvironment)
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
        var problem = Problem(
            title: exception.Error.Message,
            instance: exception.Error.Source,
            detail: exception.Error.StackTrace);
        var problemDetails = (ProblemDetails) Problem().Value!;

        problemDetails.Extensions.TryGetValue("traceId", out var traceId);
        
        _logger.LogError(@"Error: TraceId {TraceId} Status {Status} Type {Type} Message {Message} Source {Source} StackTrace {StackTrace}",
            traceId, problemDetails.Status, problemDetails.Type,
            exception.Error.Message, exception.Error.Source, exception.Error.StackTrace);

        return !hostEnvironment.IsDevelopment() ? Problem() : problem;
    }
}