using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OrderManagementSystem.DTOs.AuthDTOs;
using OrderManagementSystem.Helpers;
using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using OrderManagementSystem.Interfaces.ServiceInterfaces;
using OrderManagementSystem.Models;
using System.Security.Claims;
using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService,
            IEmailService emailService,
            IEmployeeRepository employeeRepository,
            IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _employeeRepository = employeeRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            var existingUserByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingUserByEmail != null)
            {
                return new AuthResponseDto { Success = false, Message = "Email address is already registered." };
            }

            var existingUserByUsername = await _userManager.FindByNameAsync(request.Username);
            if (existingUserByUsername != null)
            {
                return new AuthResponseDto { Success = false, Message = "Username is already taken." };
            }

            // Create corresponding Employee record
            var employee = new Employee
            {
                FirstName = request.FirstName ?? request.Username,
                LastName = request.LastName ?? "",
                FullName = $"{(request.FirstName ?? request.Username)} {request.LastName ?? ""}".Trim(),
                TelNo = "0000000000",
                Email = request.Email,
                Status = EmployeeStatus.Active,
                IsDeleted = RecordDeleteStatus.Active,
                CreatedBy = 0, // Self registered
                CreatedDate = DateTime.UtcNow
            };

            await _employeeRepository.Add(employee);
            await _employeeRepository.SaveChanges();

            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmployeeID = employee.EmployeeID,
                EmailConfirmed = true
            };

           
            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                _employeeRepository.Delete(employee);
                await _employeeRepository.SaveChanges();

                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                return new AuthResponseDto { Success = false, Message = $"Registration failed: {errors}" };
            }

            string roleName = string.IsNullOrWhiteSpace(request.Role) ? "User" : request.Role;
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
            await _userManager.AddToRoleAsync(user, roleName);

            await _emailService.SendWelcomeEmailAsync(user.Email, user.UserName);

            var roles = await _userManager.GetRolesAsync(user);
            var (accessToken, expiration) = await _tokenService.GenerateAccessTokenAsync(user, roles);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                Success = true,
                Message = "User registered successfully.",
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Expiration = expiration,
                User = new UserProfileDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmployeeID = user.EmployeeID,
                    Roles = roles.ToList()
                }
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _userManager.FindByNameAsync(request.UsernameOrEmail)
                       ?? await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (user == null)
            {
                return new AuthResponseDto { Success = false, Message = "Invalid username/email or password." };
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);
            if (signInResult.IsLockedOut)
            {
                return new AuthResponseDto { Success = false, Message = "This account has been deactivated. Please contact your administrator." };
            }
            if (!signInResult.Succeeded)
            {
                return new AuthResponseDto { Success = false, Message = "Invalid username/email or password." };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var (accessToken, expiration) = await _tokenService.GenerateAccessTokenAsync(user, roles);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                Success = true,
                Message = "Login successful.",
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Expiration = expiration,
                User = new UserProfileDto
                {
                    Id = user.Id,
                    Username = user.UserName!,
                    Email = user.Email!,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmployeeID = user.EmployeeID,
                    Roles = roles.ToList()
                }
            };
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            ClaimsPrincipal? principal;
            try
            {
                principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            }
            catch (Exception ex)
            {
                return new AuthResponseDto { Success = false, Message = $"Invalid access token: {ex.Message}" };
            }

            if (principal == null)
            {
                return new AuthResponseDto { Success = false, Message = "Invalid token claims." };
            }

            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = principal.FindFirstValue(ClaimTypes.Name);

            var user = await _userManager.FindByIdAsync(userId ?? string.Empty)
                       ?? await _userManager.FindByNameAsync(username ?? string.Empty);

            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new AuthResponseDto { Success = false, Message = "Invalid or expired refresh token." };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var (newAccessToken, expiration) = await _tokenService.GenerateAccessTokenAsync(user, roles);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                Success = true,
                Message = "Tokens refreshed successfully.",
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                Expiration = expiration,
                User = new UserProfileDto
                {
                    Id = user.Id,
                    Username = user.UserName!,
                    Email = user.Email!,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmployeeID = user.EmployeeID,
                    Roles = roles.ToList()
                }
            };
        }

        public async Task<bool> RevokeTokenAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return false;

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task<UserProfileDto?> GetUserProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            return new UserProfileDto
            {
                Id = user.Id,
                Username = user.UserName!,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmployeeID = user.EmployeeID,
                Roles = roles.ToList()
            };
        }
    }
}
