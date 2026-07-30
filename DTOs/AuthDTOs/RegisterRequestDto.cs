using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.DTOs.AuthDTOs
{
    public class RegisterRequestDto
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(6)]
        public required string Password { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Role { get; set; }
    }
}
