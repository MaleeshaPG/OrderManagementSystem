namespace OrderManagementSystem.DTOs.AuthDTOs
{
    public class AuthResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? Expiration { get; set; }
        public UserProfileDto? User { get; set; }
    }
}
