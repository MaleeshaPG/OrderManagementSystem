using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.DTOs.SubDepartmentDTOs
{
    public class CreateSubDepartmentRequest
    {
        public int DepartmentID { get; set; }
        public required string SubDepartmentName { get; set; }
        public RecordStatus Status { get; set; }
    }
}
