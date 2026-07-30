using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.DTOs.StoreDTOs
{
    public class CreateStoreRequest
    {
        public required string StoreName { get; set; }
        public required string Address { get; set; }
        public required string TelNo { get; set; }
        public required string Email { get; set; }
        public EmployeeStatus Status { get; set; }
    }
}
