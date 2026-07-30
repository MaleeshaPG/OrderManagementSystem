using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.DTOs.AuthDTOs
{
    public class RefreshTokenRequestDto
    {
        [Required]
        public required string AccessToken { get; set; }

        [Required]
        public required string RefreshToken { get; set; }
    }
}
