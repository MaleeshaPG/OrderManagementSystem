using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Models
{
    public class SubDepartment
    {

        public int SubDepartmentID { get; set; }
        public int DepartmentID { get; set; }
        public required string SubDepartmentName { get; set; }
        public RecordStatus Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public Department Department { get; set; } = null!;

        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
