using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public required string DepartmentName { get; set; }
        public RecordStatus Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ICollection<SubDepartment> SubDepartments { get; set; } = new List<SubDepartment>();
    }
}
