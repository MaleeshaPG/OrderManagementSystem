using OrderManagementSystem.DTOs.ItemDTOs;
using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using OrderManagementSystem.Interfaces.ServiceRepositories;
using OrderManagementSystem.Models;
using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Services
{
    public class ItemService : BaseService<Item>, IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
            : base(itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IEnumerable<Item>> GetBySubDepartment(int subDepartmentId)
        {
            return await _itemRepository.GetBySubDepartment(subDepartmentId);
        }

        public async Task<Item> Create(CreateItemRequest request, int createdBy)
        {
            var item = new Item
            {
                ItemName = request.ItemName,
                BaseUnit = request.BaseUnit,
                Unit = request.Unit,
                SellingPrice = request.SellingPrice,
                BaseUnitToUnitConversion = request.BaseUnitToUnitConversion,
                Status = request.Status,
                IsDeleted = RecordDeleteStatus.Active,
                SubDepartmentID = request.SubDepartmentID,
                OrderGroupID = request.OrderGroupID,
                CreatedBy = createdBy,
                CreatedDate = DateTime.UtcNow
            };

            await _repository.Add(item);
            await _repository.SaveChanges();
            return item;
        }

        public async Task<Item?> Update(int id, UpdateItemRequest request, int modifiedBy)
        {
            var item = await _repository.GetById(id);
            if (item == null) return null;

            item.ItemName = request.ItemName;
            item.BaseUnit = request.BaseUnit;
            item.Unit = request.Unit;
            item.SellingPrice = request.SellingPrice;
            item.BaseUnitToUnitConversion = request.BaseUnitToUnitConversion;
            item.Status = request.Status;
            item.SubDepartmentID = request.SubDepartmentID;
            item.OrderGroupID = request.OrderGroupID;
            item.ModifiedBy = modifiedBy;
            item.ModifiedDate = DateTime.UtcNow;

            _repository.Update(item);
            await _repository.SaveChanges();
            return item;
        }

        public async Task<bool> SoftDelete(int id)
        {
            var item = await _repository.GetById(id);
            if (item == null) return false;

            item.IsDeleted = RecordDeleteStatus.Deleted;
            _repository.Update(item);
            await _repository.SaveChanges();
            return true;
        }
    }
}
