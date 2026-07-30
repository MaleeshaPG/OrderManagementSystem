using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Repositories
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(OMSDbContext context)
            : base(context)
        {
        }

        public async Task<Store?> GetByName(string name)
        {
            return await _context.Stores
                .FirstOrDefaultAsync(x => x.StoreName == name);
        }
    }
}
