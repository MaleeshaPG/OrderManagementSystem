using OrderManagementSystem.DTOs.SupplierDTOs;
using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using OrderManagementSystem.Interfaces.ServiceRepositories;
using OrderManagementSystem.Models;
using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Services
{
    public class SupplierService : BaseService<Supplier>, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
            : base(supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<Supplier> Create(CreateSupplierRequest request, int createdBy)
        {
            var supplier = new Supplier
            {
                SupplierName = request.SupplierName,
                Address = request.Address,
                TelNo = request.TelNo,
                Email = request.Email,
                Status = request.Status,
                IsDeleted = RecordDeleteStatus.Active,
                CreatedBy = createdBy,
                CreatedDate = DateTime.UtcNow
            };

            await _repository.Add(supplier);
            await _repository.SaveChanges();
            return supplier;
        }

        public async Task<Supplier?> Update(int id, UpdateSupplierRequest request, int modifiedBy)
        {
            var supplier = await _repository.GetById(id);
            if (supplier == null) return null;

            supplier.SupplierName = request.SupplierName;
            supplier.Address = request.Address;
            supplier.TelNo = request.TelNo;
            supplier.Email = request.Email;
            supplier.Status = request.Status;
            supplier.ModifiedBy = modifiedBy;
            supplier.ModifiedDate = DateTime.UtcNow;

            _repository.Update(supplier);
            await _repository.SaveChanges();
            return supplier;
        }

        public async Task<bool> SoftDelete(int id)
        {
            var supplier = await _repository.GetById(id);
            if (supplier == null) return false;

            supplier.IsDeleted = RecordDeleteStatus.Deleted;
            _repository.Update(supplier);
            await _repository.SaveChanges();
            return true;
        }
    }
}
