using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public required string ItemName { get; set; }
        public BaseUnit BaseUnit { get; set; } 
        public Unit Unit { get; set; }
        public Decimal SellingPrice { get; set; }
        public Decimal BaseUnitToUnitConversion { get; set; }
        public RecordStatus Status { get; set; }
        public RecordDeleteStatus IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int SubDepartmentID { get; set; }
        public SubDepartment SubDepartment { get; set; } = null!;
        public int OrderGroupID { get; set; }
        public OrderGroup OrderGroup { get; set; } = null!;
        public ICollection<StoreItem> StoreItems { get; set; } = new List<StoreItem>();

    }
}
