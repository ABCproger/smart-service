namespace SmartService.Core.Services.EquipmentPlacement;

using Database;
using Database.Entities;
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
            var facility = await _dbContext.ProductionFacilities
                .Include(f => f.EquipmentPlacementContracts)
                .ThenInclude(с => с.ProcessEquipment)
                .FirstOrDefaultAsync(f => f.Code == request.ProductionFacilityCode);

            if (facility == null)   
            {
                return new ExecutionResult(new ErrorInfo("Production facility not found."));
            }
            
            var equipment = await _dbContext.ProcessEquipments
                .FirstOrDefaultAsync(e => e.Code == request.ProccessQuipmentCode);

            if (equipment == null)
            {
                return new ExecutionResult(new ErrorInfo("Process equipment not found."));
            }
            
            var occupiedArea = facility.EquipmentPlacementContracts.Sum(c => 
                c.EquipmentUnits * c.ProcessEquipment.Area);
            
            var requiredArea = request.Quantity * equipment.Area;
            
            if (occupiedArea + requiredArea > facility.StandardArea)
            {
                return new ExecutionResult(new ErrorInfo("Insufficient space in the production facility."));
            }
            
            var newContract = new EquipmentPlacementContract
            {
                ProductionFacilityCode = facility.Code,
                ProcessEquipmentCode = equipment.Code,
                EquipmentUnits = request.Quantity
            };

            _dbContext.EquipmentPlacementContracts.Add(newContract);
            await _dbContext.SaveChangesAsync();
            return new ExecutionResult(new InfoMessage("Equipment placement was created successfully"));
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