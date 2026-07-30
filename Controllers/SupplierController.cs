using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.DTOs.SupplierDTOs;
using OrderManagementSystem.Interfaces.ServiceRepositories;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class SupplierController : BaseController
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var suppliers = await _supplierService.GetAll();
            return Success(suppliers);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var supplier = await _supplierService.GetById(id);
            if (supplier == null)
                return Error("Supplier not found.", 404);

            return Success(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSupplierRequest request)
        {
            var supplier = await _supplierService.Create(request, CurrentEmployeeId);
            return Success(supplier, 201, "Supplier created successfully.");
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSupplierRequest request)
        {
            var supplier = await _supplierService.Update(id, request, CurrentEmployeeId);
            if (supplier == null)
                return Error("Supplier not found.", 404);

            return Success(supplier, 200, "Supplier updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _supplierService.SoftDelete(id);
            if (!result)
                return Error("Supplier not found.", 404);

            return Success(null!, 200, "Supplier deleted successfully.");
        }
    }
}
