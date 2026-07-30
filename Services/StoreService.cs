using OrderManagementSystem.DTOs.StoreDTOs;
using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using OrderManagementSystem.Interfaces.ServiceRepositories;
using OrderManagementSystem.Models;
using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Services
{
    public class StoreService : BaseService<Store>, IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
            : base(storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<Store> Create(CreateStoreRequest request, int createdBy)
        {
            var store = new Store
            {
                StoreName = request.StoreName,
                Address = request.Address,
                TelNo = request.TelNo,
                Email = request.Email,
                Status = request.Status,
                IsDeleted = RecordDeleteStatus.Active,
                CreatedBy = createdBy,
                CreatedDate = DateTime.UtcNow
            };

            await _repository.Add(store);
            await _repository.SaveChanges();
            return store;
        }

        public async Task<Store?> Update(int id, UpdateStoreRequest request, int modifiedBy)
        {
            var store = await _repository.GetById(id);
            if (store == null) return null;

            store.StoreName = request.StoreName;
            store.Address = request.Address;
            store.TelNo = request.TelNo;
            store.Email = request.Email;
            store.Status = request.Status;
            store.ModifiedBy = modifiedBy;
            store.ModifiedDate = DateTime.UtcNow;

            _repository.Update(store);
            await _repository.SaveChanges();
            return store;
        }

        public async Task<bool> SoftDelete(int id)
        {
            var store = await _repository.GetById(id);
            if (store == null) return false;

            store.IsDeleted = RecordDeleteStatus.Deleted;
            _repository.Update(store);
            await _repository.SaveChanges();
            return true;
        }
    }
}
