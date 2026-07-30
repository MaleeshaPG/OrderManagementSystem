using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.DTOs.OrderDTOs
{
    public class CreateOrderRequest
    {
        public required string OrderNo { get; set; }
        public OrderStatus Status { get; set; }
        public List<CreateOrderDetailRequest> OrderDetails { get; set; } = new();
    }

    public class CreateOrderDetailRequest
    {
        public int ItemID { get; set; }
        public int StoreID { get; set; }
        public int SupplierID { get; set; }
        public decimal Quantity { get; set; }
        public BaseUnit BaseUnit { get; set; }
        public Unit Unit { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal BaseUnitToUnitConversion { get; set; }
    }
}
