using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.DTOs.SupplierDTOs
{
    public class CreateSupplierRequest
    {
        public required string SupplierName { get; set; }
        public required string Address { get; set; }
        public required string TelNo { get; set; }
        public required string Email { get; set; }
        public EmployeeStatus Status { get; set; }
    }
}
