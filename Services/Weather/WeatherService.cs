using Microsoft.Extensions.Caching.Memory;

namespace AdaptiveWebInterfaces_WebAPI.Services.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;
        private const string WeatherCacheKey = "WeatherCacheKey";
        private readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

        public WeatherService(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _memoryCache = memoryCache;
            _configuration = configuration;
        }

        public async Task<string> GetWeatherAsync(string city)
        {
            if (_memoryCache.TryGetValue(WeatherCacheKey, out string cachedWeather))
            {
                return cachedWeather;
            }

            var apiKey = _configuration["OpenWeather:ApiKey"];
            var requestUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(requestUrl);

            response.EnsureSuccessStatusCode();

            var weatherData = await response.Content.ReadAsStringAsync();
            _memoryCache.Set(WeatherCacheKey, weatherData, CacheDuration);

            return weatherData;
        }
    }
}
