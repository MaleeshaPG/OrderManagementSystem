using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Models
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public int StoreID { get; set; }
        public int SupplierID { get; set; }

        public Decimal Quantity { get; set; }
        public BaseUnit BaseUnit { get; set; }
        public Unit Unit { get; set; }
        public Decimal BuyingPrice { get; set; }
        public Decimal BaseUnitToUnitConversion { get; set; }

        public Order Order { get; set; } = null!;
        public Item Item { get; set; } = null!;
        public Store Store { get; set; } = null!;
        public Supplier Supplier { get; set; } = null!;

    }
}
