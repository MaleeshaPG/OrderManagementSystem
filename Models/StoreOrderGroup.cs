namespace OrderManagementSystem.Models
{
    public class StoreOrderGroup
    {
        public int StoreID { get; set; }
        public int OrderGroupID { get; set; }
        public bool IsSunday { get; set; }
        public bool IsMonday { get; set; }
        public bool IsTuesday { get; set; }
        public bool IsWednesday { get; set; }
        public bool IsThursday { get; set; }
        public bool IsFriday { get; set; }
        public bool IsSaturday { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public Store Store { get; set; } = null!;
        public OrderGroup OrderGroup { get; set; } = null!;

    }
}
