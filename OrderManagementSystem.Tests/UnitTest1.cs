using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using OrderManagementSystem.Helpers;
using OrderManagementSystem.Models;
using OrderManagementSystem.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OrderManagementSystem.Tests;

public class TokenServiceTests
{
    private static TokenService CreateTokenService(int expirationMinutes = 60)
    {
        var jwtSettings = new JwtSettings
        {
            SecretKey = "VeryStrongSecretKeyThatIsAtLeast32Chars!",
            Issuer = "unit-test-issuer",
            Audience = "unit-test-audience",
            AccessTokenExpirationMinutes = expirationMinutes
        };

        return new TokenService(Options.Create(jwtSettings));
    }

    [Fact]
    public async Task GenerateAccessTokenAsync_IncludesExpectedClaims()
    {
        var service = CreateTokenService();
        var user = new ApplicationUser
        {
            Id = "user-123",
            UserName = "tester",
            Email = "tester@example.com",
            EmployeeID = 42
        };
        var roles = new List<string> { "Admin", "User" };

        var (token, expiration) = await service.GenerateAccessTokenAsync(user, roles);

        Assert.False(string.IsNullOrWhiteSpace(token));
        Assert.True(expiration > DateTime.UtcNow);

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

        Assert.Equal("unit-test-issuer", jwt.Issuer);
        Assert.Contains("unit-test-audience", jwt.Audiences);

        var jwtClaims = jwt.Claims.ToList();

        var nameIdClaim = jwtClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier || claim.Type == JwtRegisteredClaimNames.Sub || claim.Type == "nameid");
        Assert.NotNull(nameIdClaim);
        Assert.Equal(user.Id, nameIdClaim.Value);

        var userNameClaim = jwtClaims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.UniqueName || claim.Type == ClaimTypes.Name || claim.Type == "unique_name");
        Assert.NotNull(userNameClaim);
        Assert.Equal(user.UserName, userNameClaim.Value);

        var emailClaim = jwtClaims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email || claim.Type == JwtRegisteredClaimNames.Email || claim.Type == "email");
        Assert.NotNull(emailClaim);
        Assert.Equal(user.Email, emailClaim.Value);

        var employeeIdClaim = jwtClaims.FirstOrDefault(claim => claim.Type == "EmployeeID");
        Assert.NotNull(employeeIdClaim);
        Assert.Equal(user.EmployeeID.Value.ToString(), employeeIdClaim.Value);

        Assert.Contains(jwtClaims, claim => (claim.Type == ClaimTypes.Role || claim.Type == "role") && claim.Value == "Admin");
        Assert.Contains(jwtClaims, claim => (claim.Type == ClaimTypes.Role || claim.Type == "role") && claim.Value == "User");
    }

    [Fact]
    public void GenerateRefreshToken_ReturnsUniqueNonEmptyValues()
    {
        var service = CreateTokenService();

        var firstToken = service.GenerateRefreshToken();
        var secondToken = service.GenerateRefreshToken();

        Assert.False(string.IsNullOrWhiteSpace(firstToken));
        Assert.False(string.IsNullOrWhiteSpace(secondToken));
        Assert.NotEqual(firstToken, secondToken);
    }

    [Fact]
    public async Task GetPrincipalFromExpiredToken_ReturnsPrincipal()
    {
        var service = CreateTokenService();
        var user = new ApplicationUser
        {
            Id = "user-456",
            UserName = "tester2",
            Email = "tester2@example.com"
        };
        var roles = new List<string> { "User" };

        var (token, expiration) = await service.GenerateAccessTokenAsync(user, roles);
        var principal = service.GetPrincipalFromExpiredToken(token);

        Assert.NotNull(principal);
        Assert.Equal(user.Id, principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        Assert.Equal(user.Email, principal?.FindFirst(ClaimTypes.Email)?.Value);
        Assert.Contains(principal?.Claims, claim => claim.Type == ClaimTypes.Role && claim.Value == "User");
    }
}

public class EmailServiceTests
{
    private static EmailService CreateEmailService(EmailSettings settings)
    {
        return new EmailService(Options.Create(settings), NullLogger<EmailService>.Instance);
    }

    [Fact]
    public async Task SendWelcomeEmailAsync_Completes_WhenCredentialsMissing()
    {
        var service = CreateEmailService(new EmailSettings
        {
            SenderEmail = "sender@example.com",
            SenderName = "Order Management System",
            Username = string.Empty,
            Password = string.Empty
        });

        await service.SendWelcomeEmailAsync("recipient@example.com", "tester");
    }

    [Fact]
    public async Task SendEmployeeCredentialsEmailAsync_Completes_WhenCredentialsMissing()
    {
        var service = CreateEmailService(new EmailSettings
        {
            SenderEmail = "sender@example.com",
            SenderName = "Order Management System",
            Username = string.Empty,
            Password = string.Empty
        });

        await service.SendEmployeeCredentialsEmailAsync("recipient@example.com", "Tester User", "tester", "tempPass123");
    }
}
