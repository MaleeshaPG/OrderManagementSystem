using OrderManagementSystem.DTOs.OrderDTOs;
using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using OrderManagementSystem.Interfaces.ServiceRepositories;
using OrderManagementSystem.Models;
using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Services
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
            : base(orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order?> GetOrderDetails(int id)
        {
            return await _orderRepository.GetOrderDetails(id);
        }

        public async Task<IEnumerable<Order>> GetByStatus(OrderStatus status)
        {
            return await _orderRepository.GetByStatus(status);
        }

        public async Task<Order> Create(CreateOrderRequest request)
        {
            var order = new Order
            {
                OrderNo = request.OrderNo,
                Status = request.Status,
                OrderDetails = request.OrderDetails.Select(d => new OrderDetail
                {
                    ItemID = d.ItemID,
                    StoreID = d.StoreID,
                    SupplierID = d.SupplierID,
                    Quantity = d.Quantity,
                    BaseUnit = d.BaseUnit,
                    Unit = d.Unit,
                    BuyingPrice = d.BuyingPrice,
                    BaseUnitToUnitConversion = d.BaseUnitToUnitConversion
                }).ToList()
            };

            await _repository.Add(order);
            await _repository.SaveChanges();
            return order;
        }

        public async Task<Order?> UpdateStatus(int id, UpdateOrderRequest request)
        {
            var order = await _repository.GetById(id);
            if (order == null) return null;

            order.Status = request.Status;
            _repository.Update(order);
            await _repository.SaveChanges();
            return order;
        }
    }
}
