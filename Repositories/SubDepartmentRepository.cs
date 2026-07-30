using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Repositories
{
    public class SubDepartmentRepository : BaseRepository<SubDepartment>, ISubDepartmentRepository
    {
        public SubDepartmentRepository(OMSDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<SubDepartment>> GetByDepartment(int departmentId)
        {
            return await _context.SubDepartments
                .Where(x => x.DepartmentID == departmentId)
                .ToListAsync();
        }

        public async Task<SubDepartment?> GetWithItems(int subDepartmentId)
        {
            return await _context.SubDepartments
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.SubDepartmentID == subDepartmentId);
        }
    }
}
