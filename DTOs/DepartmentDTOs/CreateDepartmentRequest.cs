using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.DTOs.DepartmentDTOs
{
    public class CreateDepartmentRequest
    {
        public required string DepartmentName { get; set; }
        public RecordStatus Status { get; set; }
    }
}
