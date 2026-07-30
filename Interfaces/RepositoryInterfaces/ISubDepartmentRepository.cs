using OrderManagementSystem.Models;

namespace OrderManagementSystem.Interfaces.RepositoryInterfaces
{
    public interface ISubDepartmentRepository : IBaseRepository<SubDepartment>
    {
        Task<IEnumerable<SubDepartment>> GetByDepartment(int departmentId);
        Task<SubDepartment?> GetWithItems(int subDepartmentId);
    }
}
