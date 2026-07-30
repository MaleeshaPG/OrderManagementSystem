using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.DTOs.EmployeeDTOs;
using OrderManagementSystem.Interfaces.ServiceRepositories;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAll();
            return Success(employees);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetById(id);
            if (employee == null)
                return Error("Employee not found.", 404);

            return Success(employee);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest request)
        {
            var employee = await _employeeService.Create(request, CurrentEmployeeId);
            return Success(employee, 201, "Employee created successfully.");
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeRequest request)
        {
            var employee = await _employeeService.Update(id, request, CurrentEmployeeId);
            if (employee == null)
                return Error("Employee not found.", 404);

            return Success(employee, 200, "Employee updated successfully.");
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.SoftDelete(id);
            if (!result)
                return Error("Employee not found.", 404);

            return Success(null!, 200, "Employee deleted successfully.");
        }
    }
}
