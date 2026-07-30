using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using OrderManagementSystem.Models;
using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(OMSDbContext context)
           : base(context)
        {
        }

        public async Task<Order?> GetOrderDetails(
             int orderId)
        {
            return await _context.Orders
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Item)
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Store)
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Supplier)
                .FirstOrDefaultAsync(
                    x => x.OrderID == orderId
                );
        }

        public async Task<Order?> GetByOrderNo(
            string orderNo)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(
                    x => x.OrderNo == orderNo
                );
        }


        public async Task<IEnumerable<Order>> GetByStatus(
            OrderStatus status)
        {
            return await _context.Orders
                .Where(x => x.Status == status)
                .ToListAsync();
        }

    }
}
