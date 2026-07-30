using OrderManagementSystem.DTOs.DepartmentDTOs;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Interfaces.ServiceRepositories
{
    public interface IDepartmentService : IBaseService<Department>
    {
        Task<Department?> GetWithSubDepartments(int id);
        Task<Department> Create(CreateDepartmentRequest request, int createdBy);
        Task<Department?> Update(int id, UpdateDepartmentRequest request, int modifiedBy);
        Task<bool> SoftDelete(int id);
    }
}
