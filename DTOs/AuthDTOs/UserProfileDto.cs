namespace OrderManagementSystem.DTOs.AuthDTOs
{
    public class UserProfileDto
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? EmployeeID { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
