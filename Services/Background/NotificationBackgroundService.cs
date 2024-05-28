using AdaptiveWebInterfaces_WebAPI.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace AdaptiveWebInterfaces_WebAPI.Services.Background
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationBackgroundService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", "System", $"Current time: {DateTime.Now}");

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
