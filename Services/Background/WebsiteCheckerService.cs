namespace AdaptiveWebInterfaces_WebAPI.Services.Background
{
    public class WebsiteCheckerService : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<WebsiteCheckerService> _logger;
        private readonly string _url = "https://api.currencybeacon.com";
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(10);

        public WebsiteCheckerService(IHttpClientFactory httpClientFactory, ILogger<WebsiteCheckerService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var client = _httpClientFactory.CreateClient();
                    var response = await client.GetAsync(_url, stoppingToken);

                    if (response.IsSuccessStatusCode)
                    {
                        _logger.LogInformation($"{DateTime.Now}: {_url} is available.");
                    }
                    else
                    {
                        _logger.LogWarning($"{DateTime.Now}: {_url} is not available. Status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"{DateTime.Now}: Error checking {_url}");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }
        }
    }
}
