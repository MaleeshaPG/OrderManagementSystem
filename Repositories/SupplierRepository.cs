using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Repositories
{
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(OMSDbContext context)
            : base(context)
        {
        }

        public async Task<Supplier?> GetByEmail(string email)
        {
            return await _context.Suppliers
                .FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
