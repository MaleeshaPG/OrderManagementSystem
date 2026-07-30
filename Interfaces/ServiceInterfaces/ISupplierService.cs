using OrderManagementSystem.DTOs.SupplierDTOs;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Interfaces.ServiceRepositories
{
    public interface ISupplierService : IBaseService<Supplier>
    {
        Task<Supplier> Create(CreateSupplierRequest request, int createdBy);
        Task<Supplier?> Update(int id, UpdateSupplierRequest request, int modifiedBy);
        Task<bool> SoftDelete(int id);
    }
}
