using OrderManagementSystem.Models;
using System.Security.Claims;

namespace OrderManagementSystem.Interfaces.ServiceInterfaces
{
    public interface ITokenService
    {
        Task<(string Token, DateTime Expiration)> GenerateAccessTokenAsync(ApplicationUser user, IList<string> roles);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
