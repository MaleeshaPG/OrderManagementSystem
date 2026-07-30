using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.DTOs.OrderDTOs
{
    public class UpdateOrderRequest
    {
        public OrderStatus Status { get; set; }
    }
}
