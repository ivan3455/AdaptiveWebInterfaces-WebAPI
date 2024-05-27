namespace AdaptiveWebInterfaces_WebAPI.Services.Weather
{
    public interface IWeatherService
    {
        Task<string> GetWeatherAsync(string city);
    }
}
