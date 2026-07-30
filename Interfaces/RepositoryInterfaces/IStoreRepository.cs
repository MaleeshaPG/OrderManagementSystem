using OrderManagementSystem.Models;

namespace OrderManagementSystem.Interfaces.RepositoryInterfaces
{
    public interface IStoreRepository : IBaseRepository<Store>
    {
        Task<Store?> GetByName(string name);
    }
}
