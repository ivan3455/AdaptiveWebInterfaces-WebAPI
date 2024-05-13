using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.User
{
    public interface IUserService
    {
        Task<UserModel> GetUserAsync(int code);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel> CreateUserAsync(UserModel user);
        Task<UserModel> UpdateUserAsync(int code, UserModel user);
        Task<bool> DeleteUserAsync(int code);
    }
}
