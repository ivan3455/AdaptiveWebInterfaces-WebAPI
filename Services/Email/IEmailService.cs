using System.Threading.Tasks;

namespace AdaptiveWebInterfaces_WebAPI.Services
{
    public interface IEmailService
    {
        Task SendEmailNotificationAsync(int entryId);
    }
}
