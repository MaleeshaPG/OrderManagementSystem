using OrderManagementSystem.Models;

namespace OrderManagementSystem.Interfaces.RepositoryInterfaces
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        Task<Department?> GetByName(string name);
        Task<Department?> GetWithSubDepartments(int departmentId);
    }
}
