using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.DTOs.DepartmentDTOs
{
    public class UpdateDepartmentRequest
    {
        public required string DepartmentName { get; set; }
        public RecordStatus Status { get; set; }
    }
}
