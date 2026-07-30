using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.DTOs.OrderDTOs;
using OrderManagementSystem.Interfaces.ServiceRepositories;
using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager,Employee,User")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAll();
            return Success(orders);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Manager,Employee,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetById(id);
            if (order == null)
                return Error("Order not found.", 404);

            return Success(order);
        }

        [HttpGet("{id:int}/details")]
        [Authorize(Roles = "Admin,Manager,Employee,User")]
        public async Task<IActionResult> GetOrderDetails(int id)
        {
            var order = await _orderService.GetOrderDetails(id);
            if (order == null)
                return Error("Order not found.", 404);

            return Success(order);
        }

        [HttpGet("by-status/{status}")]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> GetByStatus(OrderStatus status)
        {
            var orders = await _orderService.GetByStatus(status);
            return Success(orders);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager,User")]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            var order = await _orderService.Create(request);
            return Success(order, 201, "Order created successfully.");
        }

        [HttpPut("{id:int}/status")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderRequest request)
        {
            var order = await _orderService.UpdateStatus(id, request);
            if (order == null)
                return Error("Order not found.", 404);

            return Success(order, 200, "Order status updated successfully.");
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderService.GetById(id);
            if (order == null)
                return Error("Order not found.", 404);

            _orderService.Delete(order);
            await _orderService.SaveChanges();
            return Success(null!, 200, "Order deleted successfully.");
        }
    }
}
