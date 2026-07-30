using OrderManagementSystem.Models;

namespace OrderManagementSystem.Interfaces.RepositoryInterfaces
{
    public interface ISupplierRepository : IBaseRepository<Supplier>
    {
        Task<Supplier?> GetByEmail(string email);
    }
}
