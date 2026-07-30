using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public required string OrderNo { get; set; }
        public OrderStatus Status { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    }
}
