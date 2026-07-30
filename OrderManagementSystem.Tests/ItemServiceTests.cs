using Moq;
using OrderManagementSystem.DTOs.ItemDTOs;
using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using OrderManagementSystem.Models;
using OrderManagementSystem.Services;
using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Tests;

public class ItemServiceTests
{
    [Fact]
    public async Task Create_AddsItemAndSavesChanges()
    {
        var repositoryMock = new Mock<IItemRepository>();
        repositoryMock.Setup(r => r.Add(It.IsAny<Item>())).Returns(Task.CompletedTask).Verifiable();
        repositoryMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask).Verifiable();

        var service = new ItemService(repositoryMock.Object);
        var request = new CreateItemRequest
        {
            ItemName = "Widget",
            BaseUnit = BaseUnit.Milliliter,
            Unit = Unit.Piece,
            SellingPrice = 12.5m,
            BaseUnitToUnitConversion = 1.0m,
            Status = RecordStatus.Active,
            SubDepartmentID = 10,
            OrderGroupID = 20
        };

        var result = await service.Create(request, createdBy: 200);

        Assert.Equal(request.ItemName, result.ItemName);
        Assert.Equal(request.BaseUnit, result.BaseUnit);
        Assert.Equal(request.Unit, result.Unit);
        Assert.Equal(request.SellingPrice, result.SellingPrice);
        Assert.Equal(RecordStatus.Active, result.Status);
        Assert.Equal(200, result.CreatedBy);
        Assert.Equal(request.SubDepartmentID, result.SubDepartmentID);
        Assert.Equal(request.OrderGroupID, result.OrderGroupID);

        repositoryMock.Verify(r => r.Add(It.IsAny<Item>()), Times.Once);
        repositoryMock.Verify(r => r.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task Update_ReturnsNull_WhenItemNotFound()
    {
        var repositoryMock = new Mock<IItemRepository>();
        repositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((Item?)null);

        var service = new ItemService(repositoryMock.Object);
        var request = new UpdateItemRequest
        {
            ItemName = "New Name",
            BaseUnit = BaseUnit.Milliliter,
            Unit = Unit.Litre,
            SellingPrice = 20m,
            BaseUnitToUnitConversion = 2m,
            Status = RecordStatus.Active,
            SubDepartmentID = 11,
            OrderGroupID = 21
        };

        var result = await service.Update(1, request, modifiedBy: 201);

        Assert.Null(result);
        repositoryMock.Verify(r => r.GetById(1), Times.Once);
    }

    [Fact]
    public async Task Update_UpdatesItem_WhenFound()
    {
        var item = new Item
        {
            ItemID = 7,
            ItemName = "Original",
            BaseUnit = BaseUnit.Milliliter,
            Unit = Unit.Piece,
            SellingPrice = 5m,
            BaseUnitToUnitConversion = 1m,
            Status = RecordStatus.Active,
            SubDepartmentID = 30,
            OrderGroupID = 40,
            CreatedBy = 2,
            CreatedDate = DateTime.UtcNow.AddDays(-2)
        };

        var repositoryMock = new Mock<IItemRepository>();
        repositoryMock.Setup(r => r.GetById(7)).ReturnsAsync(item);
        repositoryMock.Setup(r => r.Update(It.IsAny<Item>())).Verifiable();
        repositoryMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask).Verifiable();

        var service = new ItemService(repositoryMock.Object);
        var request = new UpdateItemRequest
        {
            ItemName = "Updated",
            BaseUnit = BaseUnit.Milliliter,
            Unit = Unit.Litre,
            SellingPrice = 8m,
            BaseUnitToUnitConversion = 0.5m,
            Status = RecordStatus.Deleted,
            SubDepartmentID = 31,
            OrderGroupID = 41
        };

        var result = await service.Update(7, request, modifiedBy: 202);

        Assert.NotNull(result);
        Assert.Equal(request.ItemName, result.ItemName);
        Assert.Equal(request.BaseUnit, result.BaseUnit);
        Assert.Equal(request.Unit, result.Unit);
        Assert.Equal(request.SellingPrice, result.SellingPrice);
        Assert.Equal(request.SubDepartmentID, result.SubDepartmentID);
        Assert.Equal(request.OrderGroupID, result.OrderGroupID);
        Assert.Equal(202, result.ModifiedBy);
        Assert.NotNull(result.ModifiedDate);

        repositoryMock.Verify(r => r.Update(It.IsAny<Item>()), Times.Once);
        repositoryMock.Verify(r => r.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task SoftDelete_ReturnsFalse_WhenItemNotFound()
    {
        var repositoryMock = new Mock<IItemRepository>();
        repositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((Item?)null);

        var service = new ItemService(repositoryMock.Object);
        var result = await service.SoftDelete(9);

        Assert.False(result);
        repositoryMock.Verify(r => r.GetById(9), Times.Once);
    }

    [Fact]
    public async Task SoftDelete_DeletesItem_WhenFound()
    {
        var item = new Item
        {
            ItemID = 8,
            ItemName = "ToDelete",
            BaseUnit = BaseUnit.Milliliter,
            Unit = Unit.Piece,
            SellingPrice = 3m,
            BaseUnitToUnitConversion = 1m,
            Status = RecordStatus.Active,
            IsDeleted = RecordDeleteStatus.Active,
            SubDepartmentID = 12,
            OrderGroupID = 22,
            CreatedBy = 2,
            CreatedDate = DateTime.UtcNow.AddDays(-3)
        };

        var repositoryMock = new Mock<IItemRepository>();
        repositoryMock.Setup(r => r.GetById(8)).ReturnsAsync(item);
        repositoryMock.Setup(r => r.Update(It.IsAny<Item>())).Verifiable();
        repositoryMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask).Verifiable();

        var service = new ItemService(repositoryMock.Object);
        var result = await service.SoftDelete(8);

        Assert.True(result);
        Assert.Equal(RecordDeleteStatus.Deleted, item.IsDeleted);
        repositoryMock.Verify(r => r.Update(It.IsAny<Item>()), Times.Once);
        repositoryMock.Verify(r => r.SaveChanges(), Times.Once);
    }
}
