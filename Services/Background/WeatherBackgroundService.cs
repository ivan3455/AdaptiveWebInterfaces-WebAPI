using AdaptiveWebInterfaces_WebAPI.Services.Weather;

namespace AdaptiveWebInterfaces_WebAPI.Services.Background
{
    public class WeatherBackgroundService : BackgroundService
    {
        private readonly IWeatherService _weatherService;

        public WeatherBackgroundService(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var weatherData = await _weatherService.GetWeatherAsync("Kyiv");
                    Console.WriteLine($"Weather data: {weatherData}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to fetch weather data: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }
    }
}
