using OrderManagementSystem.DTOs.ItemDTOs;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Interfaces.ServiceRepositories
{
    public interface IItemService : IBaseService<Item>
    {
        Task<IEnumerable<Item>> GetBySubDepartment(int subDepartmentId);
        Task<Item> Create(CreateItemRequest request, int createdBy);
        Task<Item?> Update(int id, UpdateItemRequest request, int modifiedBy);
        Task<bool> SoftDelete(int id);
    }
}
