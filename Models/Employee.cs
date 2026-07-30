using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string FullName { get; set; }
        public required string TelNo { get; set; }
        public required string Email { get; set; }
        public EmployeeStatus Status { get; set; }
        public RecordDeleteStatus IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }
    }
}
