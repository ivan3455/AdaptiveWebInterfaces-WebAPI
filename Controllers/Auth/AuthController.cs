using AdaptiveWebInterfaces_WebAPI.Models.User;
using AdaptiveWebInterfaces_WebAPI.Services.Auth;
using AdaptiveWebInterfaces_WebAPI.Services.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace AdaptiveWebInterfaces_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthService authService, IJwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            try
            {
                var user = await _authService.AuthenticateUserAsync(login.Email, login.Password);
                if (user == null)
                {
                    return Unauthorized("Invalid email or password.");
                }

                var token = _jwtService.GenerateToken(login.Email);

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel newUser)
        {
            try
            {
                var user = await _authService.RegisterUserAsync(newUser);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel changePassword)
        {
            try
            {
                var success = await _authService.ChangePasswordAsync(changePassword.UserId, changePassword.CurrentPassword, changePassword.NewPassword);
                if (!success)
                {
                    return BadRequest("Failed to change password.");
                }

                return Ok("Password changed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
