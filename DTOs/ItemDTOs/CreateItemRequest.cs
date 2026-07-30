using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.DTOs.ItemDTOs
{
    public class CreateItemRequest
    {
        public required string ItemName { get; set; }
        public BaseUnit BaseUnit { get; set; }
        public Unit Unit { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal BaseUnitToUnitConversion { get; set; }
        public RecordStatus Status { get; set; }
        public int SubDepartmentID { get; set; }
        public int OrderGroupID { get; set; }
    }
}
