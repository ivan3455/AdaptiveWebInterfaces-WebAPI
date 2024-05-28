using AdaptiveWebInterfaces_WebAPI.Services.Weather;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AdaptiveWebInterfaces_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            using (var activity = Telemetry.ActivitySource.StartActivity("GetWeather"))
            {
                activity?.SetTag("parameter.city", city);
                activity?.SetTag("user.id", User?.Identity?.Name ?? "anonymous");

                var weatherData = await _weatherService.GetWeatherAsync(city);

                activity?.SetTag("result.status", "success");
                activity?.SetTag("result.weatherData", weatherData);

                return Ok(weatherData);
            }
        }
    }
}
