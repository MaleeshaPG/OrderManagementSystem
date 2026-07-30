using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.DTOs.EmployeeDTOs
{
    public class UpdateEmployeeRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string TelNo { get; set; }
        public required string Email { get; set; }
        public EmployeeStatus Status { get; set; }
    }
}
