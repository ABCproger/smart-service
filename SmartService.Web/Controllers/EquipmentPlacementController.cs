namespace SmartService.Controllers;

using Core.Models.Dto.CreateEquipmentPlacementContract;
using Core.Services.EquipmentPlacement;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

public class EquipmentPlacementController : BaseApiController
{
    private readonly IEquipmentPlacementService _equipmentPlacementService;

    public EquipmentPlacementController(IEquipmentPlacementService equipmentPlacementService)
    {
        _equipmentPlacementService = equipmentPlacementService;
    }

    [HttpPost]
    [SwaggerOperation("Create equipment placement contract")]
    [Produces("application/json")]
    [Route("api/contracts")]
    public async Task<IActionResult> CreateEquipmentPlacementContract(
        [FromBody] CreateEquipmentPlacementContractRequestDto request)
    {
        var result = await _equipmentPlacementService.CreateEquipmentPlacementContractAsync(request);
        return FromExecutionResult(result);
    }

    [HttpGet]
    [SwaggerOperation("Get equipment placement contracts")]
    [Produces("application/json", "application/xml")]
    [Route("api/contracts")]
    public async Task<IActionResult> GetEquipmentPlacementContracts()
    {
        var result = await _equipmentPlacementService.GetEquipmentPlacementContractsAsync();
        return FromExecutionResult(result);
    }
}