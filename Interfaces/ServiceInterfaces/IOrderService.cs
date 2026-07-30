using OrderManagementSystem.DTOs.OrderDTOs;
using OrderManagementSystem.Models;
using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Interfaces.ServiceRepositories
{
    public interface IOrderService : IBaseService<Order>
    {
        Task<Order?> GetOrderDetails(int id);
        Task<IEnumerable<Order>> GetByStatus(OrderStatus status);
        Task<Order> Create(CreateOrderRequest request);
        Task<Order?> UpdateStatus(int id, UpdateOrderRequest request);
    }
}
