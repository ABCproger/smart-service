namespace SmartService.Tests;

using Core.Database;
using Core.Database.Entities;
using Core.ExecutionResult;
using Core.Models.Dto.CreateEquipmentPlacementContract;
using Core.Models.Dto.GetEquipmentPlacementContracts;
using Core.Services.EquipmentPlacement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

public class EquipmentPlacementServiceTests
{
    private readonly Mock<ILogger<EquipmentPlacementService>> _mockLogger;
    private readonly DbContextOptions<BaseDbContext> _dbContextOptions;

    public EquipmentPlacementServiceTests()
    {
        _mockLogger = new Mock<ILogger<EquipmentPlacementService>>();
        _dbContextOptions = new DbContextOptionsBuilder<BaseDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    private async Task ClearDatabaseAsync(BaseDbContext dbContext)
    {
        dbContext.ProductionFacilities.RemoveRange(dbContext.ProductionFacilities);
        dbContext.ProcessEquipments.RemoveRange(dbContext.ProcessEquipments);
        dbContext.EquipmentPlacementContracts.RemoveRange(dbContext.EquipmentPlacementContracts);
        await dbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task CreateEquipmentPlacementContractAsync_Success_ReturnsSuccess()
    {
        // Arrange
        var dbContext = new BaseDbContext(_dbContextOptions);
        var service = new EquipmentPlacementService(_mockLogger.Object, dbContext);
        
        var facility = new ProductionFacility
        {
            Code = "Facility1",
            StandardArea = 1000,
            Name = "Test Facility",
            EquipmentPlacementContracts = new List<EquipmentPlacementContract>()
        };
        
        var equipment = new ProcessEquipment
        {
            Code = "Equipment1",
            Name = "Test Equipment",
            Area = 10
        };
        
        dbContext.ProductionFacilities.Add(facility);
        dbContext.ProcessEquipments.Add(equipment);
        await dbContext.SaveChangesAsync();
        
        var request = new CreateEquipmentPlacementContractRequestDto
        {
            ProductionFacilityCode = "Facility1",
            ProccessQuipmentCode = "Equipment1",
            Quantity = 5
        };

        // Act
        var result = await service.CreateEquipmentPlacementContractAsync(request);

        // Assert
        var successResult = Assert.IsType<ExecutionResult>(result);
        Assert.True(successResult.Success);
        Assert.Contains(dbContext.EquipmentPlacementContracts,
            c => c.ProductionFacilityCode == "Facility1" && c.ProcessEquipmentCode == "Equipment1");
    }

    [Fact]
    public async Task CreateEquipmentPlacementContractAsync_FacilityNotFound_ReturnsError()
    {
        // Arrange
        var dbContext = new BaseDbContext(_dbContextOptions);
        var service = new EquipmentPlacementService(_mockLogger.Object, dbContext);

        await ClearDatabaseAsync(dbContext);

        var request = new CreateEquipmentPlacementContractRequestDto
        {
            ProductionFacilityCode = "FacilityNotExist",
            ProccessQuipmentCode = "Equipment1",
            Quantity = 5
        };

        // Act
        var result = await service.CreateEquipmentPlacementContractAsync(request);

        // Assert
        var errorResult = Assert.IsType<ExecutionResult>(result);
        Assert.False(errorResult.Success);
        Assert.Contains(errorResult.Errors, e => e.Message == "Production facility not found.");
    }

    [Fact]
    public async Task CreateEquipmentPlacementContractAsync_InsufficientSpace_ReturnsError()
    {
        // Arrange
        var dbContext = new BaseDbContext(_dbContextOptions);
        var service = new EquipmentPlacementService(_mockLogger.Object, dbContext);

        await ClearDatabaseAsync(dbContext);
        
        var facility = new ProductionFacility
        {
            Code = "Facility1",
            Name = "Test Facility",
            StandardArea = 100,
            EquipmentPlacementContracts = new List<EquipmentPlacementContract>()
        };
        
        var equipment = new ProcessEquipment
        {
            Code = "Equipment1",
            Name = "Test Equipment",
            Area = 10
        };
        
        dbContext.ProductionFacilities.Add(facility);
        dbContext.ProcessEquipments.Add(equipment);
        await dbContext.SaveChangesAsync();
        
        var request = new CreateEquipmentPlacementContractRequestDto
        {
            ProductionFacilityCode = "Facility1",
            ProccessQuipmentCode = "Equipment1",
            Quantity = 15
        };

        // Act
        var result = await service.CreateEquipmentPlacementContractAsync(request);

        // Assert
        var errorResult = Assert.IsType<ExecutionResult>(result);
        Assert.False(errorResult.Success);
        Assert.Contains(errorResult.Errors, e => e.Message == "Insufficient space in the production facility.");
    }

    [Fact]
    public async Task GetEquipmentPlacementContractsAsync_ReturnsContracts()
    {
        // Arrange
        var dbContext = new BaseDbContext(_dbContextOptions);
        var service = new EquipmentPlacementService(_mockLogger.Object, dbContext);

        await ClearDatabaseAsync(dbContext);

        var facility = new ProductionFacility
        {
            Code = "Facility1",
            Name = "Test Facility"
        };

        var equipment = new ProcessEquipment
        {
            Code = "Equipment1",
            Name = "Test Equipment",
            Area = 10
        };

        var contract = new EquipmentPlacementContract
        {
            ProductionFacilityCode = "Facility1",
            ProcessEquipmentCode = "Equipment1",
            EquipmentUnits = 5,
            ProductionFacility = facility,
            ProcessEquipment = equipment
        };

        dbContext.ProductionFacilities.Add(facility);
        dbContext.ProcessEquipments.Add(equipment);
        dbContext.EquipmentPlacementContracts.Add(contract);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await service.GetEquipmentPlacementContractsAsync();

        // Assert
        var executionResult = Assert.IsType<ExecutionResult<List<GetEquipmentPlacementContractsResponseDto>>>(result);
        var contractsList = executionResult.Value;
        Assert.NotEmpty(contractsList);
        Assert.Equal("Test Facility", contractsList.First().ProductionFacilityName);
        Assert.Equal("Test Equipment", contractsList.First().ProcessEquipmentName);
        Assert.Equal(5, contractsList.First().EquipmentQuantity);
    }

    [Fact]
    public async Task GetEquipmentPlacementContractsAsync_NoContracts_ReturnsEmptyList()
    {
        // Arrange
        var dbContext = new BaseDbContext(_dbContextOptions);
        var service = new EquipmentPlacementService(_mockLogger.Object, dbContext);

        await ClearDatabaseAsync(dbContext);

        // Act
        var result = await service.GetEquipmentPlacementContractsAsync();

        // Assert
        var executionResult = Assert.IsType<ExecutionResult<List<GetEquipmentPlacementContractsResponseDto>>>(result);
        var contractsList = executionResult.Value;
        Assert.Empty(contractsList);
    }
}
