using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Models
{
    public class OrderGroup
    {
        public int OrderGroupID { get; set; }
        public required string OrderGroupName { get; set; }
        public int LeadTime { get; set; }
        public LeadTimeType LeadTimeType { get; set; }
        public RecordStatus Status { get; set; }
        public RecordDeleteStatus IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();

        public ICollection<StoreOrderGroup> StoreOrderGroups { get; set; } = new List<StoreOrderGroup>();
    }
}
