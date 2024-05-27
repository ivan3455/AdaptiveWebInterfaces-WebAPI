namespace AdaptiveWebInterfaces_WebAPI.Services.Currency
{
    public interface ICurrencyService
    {
        Task<Dictionary<string, decimal>> GetLatestRatesAsync(string baseCurrency);
    }
}
