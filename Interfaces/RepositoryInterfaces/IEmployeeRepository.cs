using OrderManagementSystem.Models;

namespace OrderManagementSystem.Interfaces.RepositoryInterfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<Employee?> GetByEmail(string email);
        Task<Employee?> GetWithApplicationUser(int employeeId);
    }
}
