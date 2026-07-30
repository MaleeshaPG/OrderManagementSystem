using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(OMSDbContext context)
            : base(context)
        {
        }

        public async Task<Department?> GetByName(string name)
        {
            return await _context.Departments
                .FirstOrDefaultAsync(x => x.DepartmentName == name);
        }

        public async Task<Department?> GetWithSubDepartments(int departmentId)
        {
            return await _context.Departments
                .Include(x => x.SubDepartments)
                .FirstOrDefaultAsync(x => x.DepartmentID == departmentId);
        }
    }
}
