using OrderManagementSystem.DTOs.EmployeeDTOs;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Interfaces.ServiceRepositories
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        Task<Employee> Create(CreateEmployeeRequest request, int createdBy);
        Task<Employee?> Update(int id, UpdateEmployeeRequest request, int modifiedBy);
        Task<bool> SoftDelete(int id);
    }
}
