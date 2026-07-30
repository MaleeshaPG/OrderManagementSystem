using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(OMSDbContext context)
            : base(context)
        {
        }

        public async Task<Employee?> GetByEmail(string email)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Employee?> GetWithApplicationUser(int employeeId)
        {
            return await _context.Employees
                .Include(x => x.ApplicationUser)
                .FirstOrDefaultAsync(x => x.EmployeeID == employeeId);
        }
    }
}
