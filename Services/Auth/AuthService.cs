using Microsoft.EntityFrameworkCore;
using AdaptiveWebInterfaces_WebAPI.Models.User;
using AdaptiveWebInterfaces_WebAPI.Services.PasswordHasher;
using AdaptiveWebInterfaces_WebAPI.Data.Database;

namespace AdaptiveWebInterfaces_WebAPI.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(ApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserModel> AuthenticateUserAsync(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

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

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> RegisterUserAsync(RegisterModel newUser)
        {
            if (await _context.Users.AnyAsync(u => u.Email == newUser.Email))
            {
                throw new InvalidOperationException("A user with this email address already exists.");
            }

            var user = new UserModel
            {
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

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                throw new ArgumentException("User not found.", nameof(userId));
            }

            if (!_passwordHasher.VerifyPassword(currentPassword, user.Password))
            {
                throw new InvalidOperationException("Current password is incorrect.");
            }

            user.Password = _passwordHasher.HashPassword(newPassword);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
