using OrderManagementSystem.Models;

namespace OrderManagementSystem.Interfaces.RepositoryInterfaces
{
    public interface IItemRepository : IBaseRepository<Item>
    {
        Task<IEnumerable<Item>> GetBySubDepartment(int subDepartmentId);
    }
}
