using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.DTOs.AuthDTOs
{
    public class LoginRequestDto
    {
        [Required]
        public required string UsernameOrEmail { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
