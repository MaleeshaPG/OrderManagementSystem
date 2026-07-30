using OrderManagementSystem.Models;
using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Interfaces.RepositoryInterfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<Order?> GetOrderDetails(int orderId);
        Task<Order?> GetByOrderNo(string orderNo);
        Task<IEnumerable<Order>> GetByStatus(
            OrderStatus status);
    }
}
