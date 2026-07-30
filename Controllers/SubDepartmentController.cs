using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.DTOs.SubDepartmentDTOs;
using OrderManagementSystem.Interfaces.ServiceRepositories;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class SubDepartmentController : BaseController
    {
        private readonly ISubDepartmentService _subDepartmentService;

        public SubDepartmentController(ISubDepartmentService subDepartmentService)
        {
            _subDepartmentService = subDepartmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subDepartments = await _subDepartmentService.GetAll();
            return Success(subDepartments);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var subDepartment = await _subDepartmentService.GetById(id);
            if (subDepartment == null)
                return Error("SubDepartment not found.", 404);

            return Success(subDepartment);
        }

        [HttpGet("by-department/{departmentId:int}")]
        public async Task<IActionResult> GetByDepartment(int departmentId)
        {
            var subDepartments = await _subDepartmentService.GetByDepartment(departmentId);
            return Success(subDepartments);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubDepartmentRequest request)
        {
            var subDepartment = await _subDepartmentService.Create(request, CurrentEmployeeId);
            return Success(subDepartment, 201, "SubDepartment created successfully.");
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSubDepartmentRequest request)
        {
            var subDepartment = await _subDepartmentService.Update(id, request, CurrentEmployeeId);
            if (subDepartment == null)
                return Error("SubDepartment not found.", 404);

            return Success(subDepartment, 200, "SubDepartment updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _subDepartmentService.SoftDelete(id);
            if (!result)
                return Error("SubDepartment not found.", 404);

            return Success(null!, 200, "SubDepartment deleted successfully.");
        }
    }
}
