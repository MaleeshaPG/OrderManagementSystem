using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.DTOs.StoreDTOs;
using OrderManagementSystem.Interfaces.ServiceRepositories;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class StoreController : BaseController
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stores = await _storeService.GetAll();
            return Success(stores);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var store = await _storeService.GetById(id);
            if (store == null)
                return Error("Store not found.", 404);

            return Success(store);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStoreRequest request)
        {
            var store = await _storeService.Create(request, CurrentEmployeeId);
            return Success(store, 201, "Store created successfully.");
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStoreRequest request)
        {
            var store = await _storeService.Update(id, request, CurrentEmployeeId);
            if (store == null)
                return Error("Store not found.", 404);

            return Success(store, 200, "Store updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _storeService.SoftDelete(id);
            if (!result)
                return Error("Store not found.", 404);

            return Success(null!, 200, "Store deleted successfully.");
        }
    }
}
