using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Repositories
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(OMSDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Item>> GetBySubDepartment(
            int subDepartmentId)
        {
            return await _context.Items
                .Where(x => x.SubDepartmentID == subDepartmentId)
                .ToListAsync();
        }
    }
}
