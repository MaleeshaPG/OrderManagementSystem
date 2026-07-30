using Moq;
using OrderManagementSystem.DTOs.DepartmentDTOs;
using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using OrderManagementSystem.Models;
using OrderManagementSystem.Services;
using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Tests;

public class DepartmentServiceTests
{
    [Fact]
    public async Task Create_AddsDepartmentAndSavesChanges()
    {
        var repositoryMock = new Mock<IDepartmentRepository>();
        repositoryMock.Setup(r => r.Add(It.IsAny<Department>())).Returns(Task.CompletedTask).Verifiable();
        repositoryMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask).Verifiable();

        var service = new DepartmentService(repositoryMock.Object);
        var request = new CreateDepartmentRequest
        {
            DepartmentName = "Accounting",
            Status = RecordStatus.Active
        };

        var result = await service.Create(request, createdBy: 100);

        Assert.Equal(request.DepartmentName, result.DepartmentName);
        Assert.Equal(request.Status, result.Status);
        Assert.Equal(100, result.CreatedBy);
        Assert.True(result.CreatedDate <= DateTime.UtcNow);

        repositoryMock.Verify(r => r.Add(It.IsAny<Department>()), Times.Once);
        repositoryMock.Verify(r => r.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task Update_ReturnsNull_WhenDepartmentNotFound()
    {
        var repositoryMock = new Mock<IDepartmentRepository>();
        repositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((Department?)null);

        var service = new DepartmentService(repositoryMock.Object);
        var request = new UpdateDepartmentRequest
        {
            DepartmentName = "Updated",
            Status = RecordStatus.Deleted
        };

        var result = await service.Update(1, request, modifiedBy: 101);

        Assert.Null(result);
        repositoryMock.Verify(r => r.GetById(1), Times.Once);
    }

    [Fact]
    public async Task Update_UpdatesDepartment_WhenFound()
    {
        var department = new Department
        {
            DepartmentID = 2,
            DepartmentName = "Original",
            Status = RecordStatus.Active,
            CreatedBy = 1,
            CreatedDate = DateTime.UtcNow.AddDays(-1)
        };

        var repositoryMock = new Mock<IDepartmentRepository>();
        repositoryMock.Setup(r => r.GetById(2)).ReturnsAsync(department);
        repositoryMock.Setup(r => r.Update(It.IsAny<Department>())).Verifiable();
        repositoryMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask).Verifiable();

        var service = new DepartmentService(repositoryMock.Object);
        var request = new UpdateDepartmentRequest
        {
            DepartmentName = "Renamed",
            Status = RecordStatus.Deleted
        };

        var result = await service.Update(2, request, modifiedBy: 102);

        Assert.NotNull(result);
        Assert.Equal(request.DepartmentName, result.DepartmentName);
        Assert.Equal(request.Status, result.Status);
        Assert.Equal(102, result.ModifiedBy);
        Assert.NotNull(result.ModifiedDate);

        repositoryMock.Verify(r => r.Update(It.IsAny<Department>()), Times.Once);
        repositoryMock.Verify(r => r.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task SoftDelete_ReturnsFalse_WhenDepartmentNotFound()
    {
        var repositoryMock = new Mock<IDepartmentRepository>();
        repositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((Department?)null);

        var service = new DepartmentService(repositoryMock.Object);
        var result = await service.SoftDelete(5);

        Assert.False(result);
        repositoryMock.Verify(r => r.GetById(5), Times.Once);
    }

    [Fact]
    public async Task SoftDelete_DeletesDepartment_WhenFound()
    {
        var department = new Department
        {
            DepartmentID = 3,
            DepartmentName = "HR",
            Status = RecordStatus.Active,
            CreatedBy = 1,
            CreatedDate = DateTime.UtcNow.AddDays(-1)
        };

        var repositoryMock = new Mock<IDepartmentRepository>();
        repositoryMock.Setup(r => r.GetById(3)).ReturnsAsync(department);
        repositoryMock.Setup(r => r.Update(It.IsAny<Department>())).Verifiable();
        repositoryMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask).Verifiable();

        var service = new DepartmentService(repositoryMock.Object);
        var result = await service.SoftDelete(3);

        Assert.True(result);
        Assert.Equal(RecordStatus.Deleted, department.Status);
        repositoryMock.Verify(r => r.Update(It.IsAny<Department>()), Times.Once);
        repositoryMock.Verify(r => r.SaveChanges(), Times.Once);
    }
}
