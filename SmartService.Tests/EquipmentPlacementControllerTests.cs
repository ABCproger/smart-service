namespace SmartService.Tests;

using System.Net;
using BackgroundServices;
using Controllers;
using Core.ExecutionResult;
using Core.Models.Dto.CreateEquipmentPlacementContract;
using Core.Models.Dto.GetEquipmentPlacementContracts;
using Core.Services.EquipmentPlacement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

public class EquipmentPlacementControllerTests
{
    private readonly Mock<IEquipmentPlacementService> _mockService;
    private readonly Mock<ILogger<EquipmentPlacementController>> _mockLogger;
    private readonly Mock<IBackgroundTaskQueue> _mockBackgroundTaskQueue;
    private readonly EquipmentPlacementController _controller;

    public EquipmentPlacementControllerTests()
    {
        _mockService = new Mock<IEquipmentPlacementService>();
        _mockLogger = new Mock<ILogger<EquipmentPlacementController>>();
        _mockBackgroundTaskQueue = new Mock<IBackgroundTaskQueue>();
        _controller = new EquipmentPlacementController(
            _mockService.Object,
            _mockLogger.Object,
            _mockBackgroundTaskQueue.Object
        );
    }

    [Fact]
    public async Task CreateEquipmentPlacementContract_Success_ReturnsOk()
    {
        // Arrange
        var request = new CreateEquipmentPlacementContractRequestDto
        {
            ProductionFacilityCode = "Facility1",
            ProccessQuipmentCode = "Equipment1",
            Quantity = 10
        };

        var successResult = new ExecutionResult(new InfoMessage("Contract created successfully"));

        _mockService
            .Setup(s => s.CreateEquipmentPlacementContractAsync(request))
            .ReturnsAsync(successResult);

        // Act
        var result = await _controller.CreateEquipmentPlacementContract(request);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var value = actionResult.Value as ExecutionResult;
        Assert.NotNull(value);
        Assert.True(value.Success);
        _mockBackgroundTaskQueue.Verify(x => x.QueueBackgroundWorkItem(It.IsAny<Func<CancellationToken, Task>>()),
            Times.Once);
    }

    [Fact]
    public async Task CreateEquipmentPlacementContract_Failure_ReturnsBadRequest()
    {
        // Arrange
        var request = new CreateEquipmentPlacementContractRequestDto
        {
            ProductionFacilityCode = "Facility1",
            ProccessQuipmentCode = "Equipment1",
            Quantity = 10
        };

        var errorResult = new ExecutionResult(new ErrorInfo("Insufficient space"));

        _mockService
            .Setup(s => s.CreateEquipmentPlacementContractAsync(request))
            .ReturnsAsync(errorResult);

        // Act
        var result = await _controller.CreateEquipmentPlacementContract(request);

        // Assert
        var actionResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.BadRequest, actionResult.StatusCode);
        var value = actionResult.Value as ExecutionResult;
        Assert.NotNull(value);
        Assert.False(value.Success);
    }

    [Fact]
    public async Task GetEquipmentPlacementContracts_ReturnsOk()
    {
        // Arrange
        var contracts = new List<GetEquipmentPlacementContractsResponseDto>
        {
            new GetEquipmentPlacementContractsResponseDto
            {
                ProductionFacilityName = "Facility1",
                ProcessEquipmentName = "Equipment1",
                EquipmentQuantity = 10
            }
        };

        var successResult = new ExecutionResult<List<GetEquipmentPlacementContractsResponseDto>>(contracts);

        _mockService
            .Setup(s => s.GetEquipmentPlacementContractsAsync())
            .ReturnsAsync(successResult);

        // Act
        var result = await _controller.GetEquipmentPlacementContracts();

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var value = actionResult.Value as ExecutionResult<List<GetEquipmentPlacementContractsResponseDto>>;
        Assert.NotNull(value);
        Assert.True(value.Success);
        Assert.Single(value.Value);
    }

    [Fact]
    public async Task GetEquipmentPlacementContracts_Failure_ReturnsBadRequest()
    {
        // Arrange
        var errorResult = new ExecutionResult<List<GetEquipmentPlacementContractsResponseDto>>(
            new ErrorInfo("Failed to fetch contracts"));

        _mockService
            .Setup(s => s.GetEquipmentPlacementContractsAsync())
            .ReturnsAsync(errorResult);

        // Act
        var result = await _controller.GetEquipmentPlacementContracts();

        // Assert
        var actionResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.BadRequest, actionResult.StatusCode);
        var value = actionResult.Value as ExecutionResult<List<GetEquipmentPlacementContractsResponseDto>>;
        Assert.NotNull(value);
        Assert.False(value.Success);
    }
}