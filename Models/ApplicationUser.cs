using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace OrderManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? EmployeeID { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        [JsonIgnore]
        public Employee? Employee { get; set; }
    }
}
