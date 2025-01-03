namespace SmartService.Core.Services.EquipmentPlacement;

using Database;
using ExecutionResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Dto.CreateEquipmentPlacementContract;
using Models.Dto.GetEquipmentPlacementContracts;

public class EquipmentPlacementService : IEquipmentPlacementService
{
    private readonly ILogger<EquipmentPlacementService> _logger;
    private readonly BaseDbContext _dbContext;

    public EquipmentPlacementService(
        ILogger<EquipmentPlacementService> logger,
        BaseDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    public async  Task<ExecutionResult> CreateEquipmentPlacementContractAsync(CreateEquipmentPlacementContractRequestDto request)
    {
        try
        {
            return new ExecutionResult();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new ExecutionResult(
                new ErrorInfo($"Error while executing {nameof(CreateEquipmentPlacementContractAsync)}"));
        }
    }

    public async Task<ExecutionResult<List<GetEquipmentPlacementContractsResponseDto>>> GetEquipmentPlacementContractsAsync()
    {
        try
        {
            var contracts = await _dbContext.EquipmentPlacementContracts
                .Select(c => new GetEquipmentPlacementContractsResponseDto
                {
                    ProductionFacilityName = c.ProductionFacility.Name,
                    ProcessEquipmentName = c.ProcessEquipment.Name,
                    EquipmentQuantity = c.EquipmentUnits
                })
                .ToListAsync();

            return new ExecutionResult<List<GetEquipmentPlacementContractsResponseDto>>(contracts);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new ExecutionResult<List<GetEquipmentPlacementContractsResponseDto>>(
                new ErrorInfo($"Error while executing {nameof(CreateEquipmentPlacementContractAsync)}"));
        }
    }
}