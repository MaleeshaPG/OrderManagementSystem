using OrderManagementSystem.DTOs.AuthDTOs;

namespace OrderManagementSystem.Interfaces.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
        Task<bool> RevokeTokenAsync(string username);
        Task<UserProfileDto?> GetUserProfileAsync(string userId);
    }
}
