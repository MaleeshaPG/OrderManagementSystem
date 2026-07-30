using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.DTOs.DepartmentDTOs;
using OrderManagementSystem.Interfaces.ServiceRepositories;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _departmentService.GetAll();
            return Success(departments);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _departmentService.GetById(id);
            if (department == null)
                return Error("Department not found.", 404);

            return Success(department);
        }

        [HttpGet("{id:int}/sub-departments")]
        public async Task<IActionResult> GetWithSubDepartments(int id)
        {
            var department = await _departmentService.GetWithSubDepartments(id);
            if (department == null)
                return Error("Department not found.", 404);

            return Success(department);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentRequest request)
        {
            var department = await _departmentService.Create(request, CurrentEmployeeId);
            return Success(department, 201, "Department created successfully.");
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDepartmentRequest request)
        {
            var department = await _departmentService.Update(id, request, CurrentEmployeeId);
            if (department == null)
                return Error("Department not found.", 404);

            return Success(department, 200, "Department updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _departmentService.SoftDelete(id);
            if (!result)
                return Error("Department not found.", 404);

            return Success(null!, 200, "Department deleted successfully.");
        }
    }
}
