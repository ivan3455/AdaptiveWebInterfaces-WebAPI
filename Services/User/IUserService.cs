using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.User
{
    public interface IUserService
    {
        Task<ResponseModel<UserModel>> GetUserAsync(int code);
        Task<ResponseModel<IEnumerable<UserModel>>> GetAllUsersAsync();
        Task<ResponseModel<UserModel>> CreateUserAsync(UserModel user);
        Task<ResponseModel<UserModel>> UpdateUserAsync(int code, UserModel user);
        Task<ResponseModel<bool>> DeleteUserAsync(int code);
    }
}
