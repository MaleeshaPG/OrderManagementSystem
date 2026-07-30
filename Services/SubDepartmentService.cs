using OrderManagementSystem.DTOs.SubDepartmentDTOs;
using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using OrderManagementSystem.Interfaces.ServiceRepositories;
using OrderManagementSystem.Models;
using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Services
{
    public class SubDepartmentService : BaseService<SubDepartment>, ISubDepartmentService
    {
        private readonly ISubDepartmentRepository _subDepartmentRepository;

        public SubDepartmentService(ISubDepartmentRepository subDepartmentRepository)
            : base(subDepartmentRepository)
        {
            _subDepartmentRepository = subDepartmentRepository;
        }

        public async Task<IEnumerable<SubDepartment>> GetByDepartment(int departmentId)
        {
            return await _subDepartmentRepository.GetByDepartment(departmentId);
        }

        public async Task<SubDepartment> Create(CreateSubDepartmentRequest request, int createdBy)
        {
            var subDepartment = new SubDepartment
            {
                DepartmentID = request.DepartmentID,
                SubDepartmentName = request.SubDepartmentName,
                Status = request.Status,
                CreatedBy = createdBy,
                CreatedDate = DateTime.UtcNow
            };

            await _repository.Add(subDepartment);
            await _repository.SaveChanges();
            return subDepartment;
        }

        public async Task<SubDepartment?> Update(int id, UpdateSubDepartmentRequest request, int modifiedBy)
        {
            var subDepartment = await _repository.GetById(id);
            if (subDepartment == null) return null;

            subDepartment.DepartmentID = request.DepartmentID;
            subDepartment.SubDepartmentName = request.SubDepartmentName;
            subDepartment.Status = request.Status;
            subDepartment.ModifiedBy = modifiedBy;
            subDepartment.ModifiedDate = DateTime.UtcNow;

            _repository.Update(subDepartment);
            await _repository.SaveChanges();
            return subDepartment;
        }

        public async Task<bool> SoftDelete(int id)
        {
            var subDepartment = await _repository.GetById(id);
            if (subDepartment == null) return false;

            subDepartment.Status = RecordStatus.Deleted;
            _repository.Update(subDepartment);
            await _repository.SaveChanges();
            return true;
        }
    }
}
