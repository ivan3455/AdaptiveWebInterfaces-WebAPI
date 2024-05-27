using AdaptiveWebInterfaces_WebAPI.Models.User;

namespace AdaptiveWebInterfaces_WebAPI.Services.Auth
{
    public interface IAuthService
    {
        Task<UserModel> RegisterUserAsync(RegisterModel newUser);
        Task<UserModel> AuthenticateUserAsync(string email, string password);
        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
    }
}
