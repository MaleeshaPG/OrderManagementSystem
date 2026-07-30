using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public required string SupplierName { get; set; }
        public required string Address { get; set; }
        public required string TelNo { get; set; }
        public required string Email { get; set; }
        public EmployeeStatus Status { get; set; }
        public RecordDeleteStatus IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ICollection<StoreItemSupplier> StoreItemSuppliers { get; set; } = new List<StoreItemSupplier>();
    }
}
