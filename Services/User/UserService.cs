namespace AdaptiveWebInterfaces_WebAPI.Services.User
{
    using AdaptiveWebInterfaces_WebAPI.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task<UserModel> GetUserAsync(int id)
        {
            return await Task.FromResult(_users.FirstOrDefault(u => u.UserId == id));
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            return await Task.FromResult(_users);
        }

        public async Task<UserModel> CreateUserAsync(UserModel user)
        {
            if (_users.Any(u => u.UserId == user.UserId))
            {
                throw new Exception("User with the same code already exists.");
            }

            _users.Add(user);
            return await Task.FromResult(user);
        }

        public async Task<UserModel> UpdateUserAsync(int code, UserModel user)
        {
            var existingUser = _users.FirstOrDefault(u => u.UserId == code);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            existingUser.LastName = user.LastName;
            existingUser.FirstName = user.FirstName;
            existingUser.Password = user.Password;
            existingUser.Address = user.Address;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.Email = user.Email;

            return await Task.FromResult(existingUser);
        }

        public async Task<bool> DeleteUserAsync(int code)
        {
            var existingUser = _users.FirstOrDefault(u => u.UserId == code);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            _users.Remove(existingUser);
            return await Task.FromResult(true);
        }
    }
}
