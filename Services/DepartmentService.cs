using OrderManagementSystem.DTOs.DepartmentDTOs;
using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using OrderManagementSystem.Interfaces.ServiceRepositories;
using OrderManagementSystem.Models;
using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Services
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
            : base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Department?> GetWithSubDepartments(int id)
        {
            return await _departmentRepository.GetWithSubDepartments(id);
        }

        public async Task<Department> Create(CreateDepartmentRequest request, int createdBy)
        {
            var department = new Department
            {
                DepartmentName = request.DepartmentName,
                Status = request.Status,
                CreatedBy = createdBy,
                CreatedDate = DateTime.UtcNow
            };

            await _repository.Add(department);
            await _repository.SaveChanges();
            return department;
        }

        public async Task<Department?> Update(int id, UpdateDepartmentRequest request, int modifiedBy)
        {
            var department = await _repository.GetById(id);
            if (department == null) return null;

            department.DepartmentName = request.DepartmentName;
            department.Status = request.Status;
            department.ModifiedBy = modifiedBy;
            department.ModifiedDate = DateTime.UtcNow;

            _repository.Update(department);
            await _repository.SaveChanges();
            return department;
        }

        public async Task<bool> SoftDelete(int id)
        {
            var department = await _repository.GetById(id);
            if (department == null) return false;

            department.Status = RecordStatus.Deleted;
            _repository.Update(department);
            await _repository.SaveChanges();
            return true;
        }
    }
}
