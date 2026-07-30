using OrderManagementSystem.DTOs.StoreDTOs;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Interfaces.ServiceRepositories
{
    public interface IStoreService : IBaseService<Store>
    {
        Task<Store> Create(CreateStoreRequest request, int createdBy);
        Task<Store?> Update(int id, UpdateStoreRequest request, int modifiedBy);
        Task<bool> SoftDelete(int id);
    }
}
