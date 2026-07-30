using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Models
{
    public class Store
    {
        public int StoreID { get; set; }
        public required string StoreName { get; set; }
        public required string Address { get; set; }
        public required string TelNo { get; set; }
        public required string Email { get; set; }
        public EmployeeStatus Status { get; set; }
        public RecordDeleteStatus IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ICollection<StoreOrderGroup> StoreOrderGroups { get; set; } = new List<StoreOrderGroup>();
        public ICollection<StoreItem> StoreItems { get; set; } = new List<StoreItem>();
    }
}
