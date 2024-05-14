using AdaptiveWebInterfaces_WebAPI.Data.User;
using AdaptiveWebInterfaces_WebAPI.Models.User;
using AdaptiveWebInterfaces_WebAPI.Services.PasswordHasher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaptiveWebInterfaces_WebAPI.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly List<UserModel> _users;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IPasswordHasher passwordHasher)
        {
            _users = TestUserData.Users;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserModel> AuthenticateUserAsync(string email, string password)
        {
            var user = _users.SingleOrDefault(u => u.Email == email);

            if (user == null || user.IsBlocked)
            {
                throw new InvalidOperationException("User not found or account is blocked.");
            }

            if (!_passwordHasher.VerifyPassword(password, user.Password))
            {
                user.FailedLoginAttempts++;
                if (user.FailedLoginAttempts >= 5)
                {
                    user.IsBlocked = true;
                }
                throw new InvalidOperationException("Incorrect password.");
            }

            user.FailedLoginAttempts = 0;
            user.LastLoginDate = DateTime.Now;
            return user;
        }

        public async Task<UserModel> RegisterUserAsync(RegisterModel newUser)
        {
            if (_users.Any(u => u.Email == newUser.Email))
            {
                throw new InvalidOperationException("A user with this email address already exists.");
            }

            var user = new UserModel
            {
                UserId = GenerateUserId(),
                LastName = newUser.LastName,
                FirstName = newUser.FirstName,
                Email = newUser.Email,
                DateOfBirth = newUser.DateOfBirth,
                Password = _passwordHasher.HashPassword(newUser.Password),
                Address = newUser.Address,
                PhoneNumber = newUser.PhoneNumber,
                LastLoginDate = DateTime.Now,
                FailedLoginAttempts = 0,
                IsBlocked = false
            };

            _users.Add(user);
            return user;
        }

        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = _users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                throw new ArgumentException("User not found.", nameof(userId));
            }

            if (!_passwordHasher.VerifyPassword(currentPassword, user.Password))
            {
                throw new InvalidOperationException("Current password is incorrect.");
            }

            user.Password = _passwordHasher.HashPassword(newPassword);
            return true;
        }

        private int GenerateUserId()
        {
            if (_users.Count == 0)
            {
                return 1;
            }
            else
            {
                int maxUserId = _users.Max(u => u.UserId);
                return maxUserId + 1;
            }
        }
    }
}
