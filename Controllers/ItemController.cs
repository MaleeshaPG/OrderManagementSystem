using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.DTOs.ItemDTOs;
using OrderManagementSystem.Interfaces.ServiceRepositories;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ItemController : BaseController
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _itemService.GetAll();
            return Success(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _itemService.GetById(id);
            if (item == null)
                return Error("Item not found.", 404);

            return Success(item);
        }

        [HttpGet("by-sub-department/{subDepartmentId:int}")]
        public async Task<IActionResult> GetBySubDepartment(int subDepartmentId)
        {
            var items = await _itemService.GetBySubDepartment(subDepartmentId);
            return Success(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateItemRequest request)
        {
            var item = await _itemService.Create(request, CurrentEmployeeId);
            return Success(item, 201, "Item created successfully.");
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateItemRequest request)
        {
            var item = await _itemService.Update(id, request, CurrentEmployeeId);
            if (item == null)
                return Error("Item not found.", 404);

            return Success(item, 200, "Item updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _itemService.SoftDelete(id);
            if (!result)
                return Error("Item not found.", 404);

            return Success(null!, 200, "Item deleted successfully.");
        }
    }
}
