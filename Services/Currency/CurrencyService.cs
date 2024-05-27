using Newtonsoft.Json.Linq;

namespace AdaptiveWebInterfaces_WebAPI.Services.Currency
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public CurrencyService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        public async Task<Dictionary<string, decimal>> GetLatestRatesAsync(string baseCurrency)
        {
            var apiKey = _configuration["CurrencyBeacon:ApiKey"];
            var baseUrl = _configuration["CurrencyBeacon:BaseUrl"];

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(baseUrl))
            {
                throw new InvalidOperationException("Currency API key or base URL is not configured.");
            }

            var requestUri = $"{baseUrl}?api_key={apiKey}&base={baseCurrency}";
            var response = await _httpClient.GetStringAsync(requestUri);

            var rates = JObject.Parse(response)["response"]["rates"].ToObject<Dictionary<string, decimal>>();
            return rates;
        }
    }
}
