using Microsoft.AspNetCore.SignalR;
using MySqlX.XDevAPI;

namespace AdaptiveWebInterfaces_WebAPI.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
