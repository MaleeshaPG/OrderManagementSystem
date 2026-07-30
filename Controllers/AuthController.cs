using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using OrderManagementSystem.DTOs.AuthDTOs;
using OrderManagementSystem.Interfaces.ServiceInterfaces;
using System.Security.Claims;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [EnableRateLimiting("AuthPolicy")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return Error("Invalid registration request data.", 400);
            }

            var result = await _authService.RegisterAsync(request);
            if (!result.Success)
            {
                return Error(result.Message, 400);
            }

            return Success(result, 201, "User registered successfully");
        }

        [HttpPost("login")]
        [EnableRateLimiting("AuthPolicy")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return Error("Invalid login request data.", 400);
            }

            var result = await _authService.LoginAsync(request);
            if (!result.Success)
            {
                return Error(result.Message, 401);
            }

            return Success(result, 200, "Login successful");
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return Error("Invalid refresh token request data.", 400);
            }

            var result = await _authService.RefreshTokenAsync(request);
            if (!result.Success)
            {
                return Error(result.Message, 400);
            }

            return Success(result, 200, "Token refreshed successfully");
        }

        [HttpPost("revoke")]
        [Authorize]
        public async Task<IActionResult> Revoke()
        {
            var username = User.FindFirstValue(ClaimTypes.Name) ?? CurrentUserName;
            if (string.IsNullOrEmpty(username))
            {
                return Error("User identity not found.", 400);
            }

            var success = await _authService.RevokeTokenAsync(username);
            if (!success)
            {
                return Error("User not found or refresh token already revoked.", 404);
            }

            return Success(new { }, 200, "Refresh token revoked successfully");
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Error("Unauthorized user context.", 401);
            }

            var profile = await _authService.GetUserProfileAsync(userId);
            if (profile == null)
            {
                return Error("User profile not found.", 404);
            }

            return Success(profile, 200, "User profile retrieved successfully");
        }
    }
}
