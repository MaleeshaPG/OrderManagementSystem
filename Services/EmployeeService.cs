using Microsoft.AspNetCore.Identity;
using OrderManagementSystem.DTOs.EmployeeDTOs;
using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using OrderManagementSystem.Interfaces.ServiceInterfaces;
using OrderManagementSystem.Interfaces.ServiceRepositories;
using OrderManagementSystem.Models;
using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService)
            : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        public async Task<Employee> Create(CreateEmployeeRequest request, int createdBy)
        {
           
            var employee = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                FullName = $"{request.FirstName} {request.LastName}",
                TelNo = request.TelNo,
                Email = request.Email,
                Status = request.Status,
                IsDeleted = RecordDeleteStatus.Active,
                CreatedBy = createdBy,
                CreatedDate = DateTime.UtcNow
            };

            await _repository.Add(employee);
            await _repository.SaveChanges();

            var tempPassword = $"{Guid.NewGuid().ToString("N")[..8]}Aa1!";

            var identityUser = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmployeeID = employee.EmployeeID,
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(identityUser, tempPassword);
            if (!createResult.Succeeded)
            {
                _repository.Update(employee);
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to create Identity account for employee: {errors}");
            }

            if (!await _roleManager.RoleExistsAsync("Employee"))
                await _roleManager.CreateAsync(new IdentityRole("Employee"));

            await _userManager.AddToRoleAsync(identityUser, "Employee");

            await _emailService.SendEmployeeCredentialsEmailAsync(
                request.Email,
                employee.FullName,
                request.Email,
                tempPassword);

            return employee;
        }

        public async Task<Employee?> Update(int id, UpdateEmployeeRequest request, int modifiedBy)
        {
            var employee = await _repository.GetById(id);
            if (employee == null) return null;

            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.FullName = $"{request.FirstName} {request.LastName}";
            employee.TelNo = request.TelNo;
            employee.Email = request.Email;
            employee.Status = request.Status;
            employee.ModifiedBy = modifiedBy;
            employee.ModifiedDate = DateTime.UtcNow;

            _repository.Update(employee);
            await _repository.SaveChanges();
            return employee;
        }

        public async Task<bool> SoftDelete(int id)
        {
            var employee = await _repository.GetById(id);
            if (employee == null) return false;

            employee.IsDeleted = RecordDeleteStatus.Deleted;
            _repository.Update(employee);
            await _repository.SaveChanges();

            var identityUser = await _userManager.FindByEmailAsync(employee.Email);
            if (identityUser != null)
            {
                identityUser.LockoutEnabled = true;
                identityUser.LockoutEnd = DateTimeOffset.MaxValue;
                await _userManager.UpdateAsync(identityUser);
            }

            return true;
        }
    }
}
