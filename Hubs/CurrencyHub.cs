using AdaptiveWebInterfaces_WebAPI.Services.Currency;
using Microsoft.AspNetCore.SignalR;
using MySqlX.XDevAPI;

namespace AdaptiveWebInterfaces_WebAPI.Hubs
{
    public class CurrencyHub : Hub
    {
        private readonly ICurrencyService _currencyService;
        private static string _baseCurrency = "USD";

        public CurrencyHub(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        public async Task SendRates()
        {
            var rates = await _currencyService.GetLatestRatesAsync(_baseCurrency);
            await Clients.All.SendAsync("ReceiveRates", rates);
        }

        public async Task SetBaseCurrency(string baseCurrency)
        {
            _baseCurrency = baseCurrency;
            await SendRates();
        }
    }
}
