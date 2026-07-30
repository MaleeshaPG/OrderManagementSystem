using OrderManagementSystem.DTOs.SubDepartmentDTOs;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Interfaces.ServiceRepositories
{
    public interface ISubDepartmentService : IBaseService<SubDepartment>
    {
        Task<IEnumerable<SubDepartment>> GetByDepartment(int departmentId);
        Task<SubDepartment> Create(CreateSubDepartmentRequest request, int createdBy);
        Task<SubDepartment?> Update(int id, UpdateSubDepartmentRequest request, int modifiedBy);
        Task<bool> SoftDelete(int id);
    }
}
