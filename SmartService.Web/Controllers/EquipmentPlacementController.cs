namespace SmartService.Controllers;

using Core.Models.Dto.CreateEquipmentPlacementContract;
using Core.Services.EquipmentPlacement;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
public class EquipmentPlacementController : ControllerBase
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
        try
        {
            await _equipmentPlacementService.CreateEquipmentPlacementContractAsync(request);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = e.Message });
        }
    }

    [HttpGet]
    [SwaggerOperation("Get equipment placement contracts")]
    [Produces("application/json", "application/xml")]
    [Route("api/contracts")]
    public async Task<IActionResult> GetEquipmentPlacementContracts()
    {
        try
        {
            var result = await _equipmentPlacementService.GetEquipmentPlacementContractsAsync();
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = e.Message });
        }
    }
}