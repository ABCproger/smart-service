namespace SmartService.Controllers;

using BackgroundServices;
using Core.Models.Dto.CreateEquipmentPlacementContract;
using Core.Services.EquipmentPlacement;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

public class EquipmentPlacementController : BaseApiController
{
    private readonly IEquipmentPlacementService _equipmentPlacementService;
    private readonly ILogger<EquipmentPlacementController> _logger;
    private readonly IBackgroundTaskQueue _backgroundTaskQueue;
    public EquipmentPlacementController(
        IEquipmentPlacementService equipmentPlacementService,
        ILogger<EquipmentPlacementController> logger,
        IBackgroundTaskQueue backgroundTaskQueue)
    {
        _equipmentPlacementService = equipmentPlacementService;
        _logger = logger;
        _backgroundTaskQueue = backgroundTaskQueue;
    }

    [HttpPost]
    [SwaggerOperation("Create equipment placement contract")]
    [Produces("application/json")]
    [Route("api/contracts")]
    public async Task<IActionResult> CreateEquipmentPlacementContract(
        [FromBody] CreateEquipmentPlacementContractRequestDto request)
    {
        var result = await _equipmentPlacementService.CreateEquipmentPlacementContractAsync(request);
        
        _logger.LogInformation(result.Success 
            ? $"Contract created for production facility with code: {request.ProductionFacilityCode}." 
            : $"Failed to create contract for production facility with code:{request.ProductionFacilityCode}.");
        
        _backgroundTaskQueue.QueueBackgroundWorkItem(async token =>
        {
            await Task.Delay(3000, token);
            _logger.LogInformation($"Background task completed for production facility with code: {request.ProductionFacilityCode}.");
        });
        
        return FromExecutionResult(result);
    }

    [HttpGet]
    [SwaggerOperation("Get equipment placement contracts")]
    [Produces("application/json", "application/xml")]
    [Route("api/contracts")]
    public async Task<IActionResult> GetEquipmentPlacementContracts()
    {
        var result = await _equipmentPlacementService.GetEquipmentPlacementContractsAsync();
        _backgroundTaskQueue.QueueBackgroundWorkItem(async token =>
        {
            await Task.Delay(3000, token);
            _logger.LogInformation($"Background task completed for get endpoint");
        });
        return FromExecutionResult(result);
    }
}