using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Security;
using Security.Entities;
using WebApi.Filters;

namespace WebApi.Controllers;

[ApiController]
[AuthFilter(Permission.ViewWeather)]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ISessionService _sessionService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ISessionService sessionService)
    {
        _logger = logger;
        this._sessionService = sessionService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        if (this._sessionService.GetCurrentUser().Email == "admin@admin.com")
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        else
        {
            return Enumerable.Range(1, 7).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
