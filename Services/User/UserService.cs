using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.User
{
    public class UserService : IUserService
    {
        private readonly List<UserModel> _users;

        public UserService()
        {
            _users = new List<UserModel>
            {
                new UserModel { UserId = 1, LastName = "Doe", FirstName = "John", Password = "password1", Address = "123 Main St", PhoneNumber = "555-1234", Email = "john@example.com" },
                new UserModel { UserId = 2, LastName = "Smith", FirstName = "Jane", Password = "password2", Address = "456 Oak St", PhoneNumber = "555-5678", Email = "jane@example.com" }
            };
        }

        public async Task<ResponseModel<UserModel>> GetUserAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.UserId == id);
            if (user != null)
            {
                return new ResponseModel<UserModel>
                {
                    Data = user,
                    Success = true,
                    Message = "User found."
                };
            }
            else
            {
                return new ResponseModel<UserModel>
                {
                    Data = null,
                    Success = false,
                    Message = "User not found."
                };
            }
        }

        public async Task<ResponseModel<IEnumerable<UserModel>>> GetAllUsersAsync()
        {
            return new ResponseModel<IEnumerable<UserModel>>
            {
                Data = _users,
                Success = true,
                Message = "All users retrieved."
            };
        }

        public async Task<ResponseModel<UserModel>> CreateUserAsync(UserModel user)
        {
            if (_users.Any(u => u.UserId == user.UserId))
            {
                return new ResponseModel<UserModel>
                {
                    Data = null,
                    Success = false,
                    Message = "User with the same code already exists."
                };
            }

            _users.Add(user);
            return new ResponseModel<UserModel>
            {
                Data = user,
                Success = true,
                Message = "User added successfully."
            };
        }

        public async Task<ResponseModel<UserModel>> UpdateUserAsync(int id, UserModel user)
        {
            var existingUser = _users.FirstOrDefault(u => u.UserId == id);
            if (existingUser == null)
            {
                return new ResponseModel<UserModel>
                {
                    Data = null,
                    Success = false,
                    Message = "User not found."
                };
            }

            existingUser.LastName = user.LastName;
            existingUser.FirstName = user.FirstName;
            existingUser.Password = user.Password;
            existingUser.Address = user.Address;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.Email = user.Email;

            return new ResponseModel<UserModel>
            {
                Data = existingUser,
                Success = true,
                Message = "User updated successfully."
            };
        }

        public async Task<ResponseModel<bool>> DeleteUserAsync(int id)
        {
            var existingUser = _users.FirstOrDefault(u => u.UserId == id);
            if (existingUser == null)
            {
                return new ResponseModel<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "User not found."
                };
            }

            _users.Remove(existingUser);
            return new ResponseModel<bool>
            {
                Data = true,
                Success = true,
                Message = "User deleted successfully."
            };
        }
    }
}
