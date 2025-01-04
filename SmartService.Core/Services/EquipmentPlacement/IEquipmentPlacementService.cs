namespace SmartService.Core.Services.EquipmentPlacement;

using ExecutionResult;
using Models.Dto.CreateEquipmentPlacementContract;
using Models.Dto.GetEquipmentPlacementContracts;

public interface IEquipmentPlacementService
{
    Task<ExecutionResult> CreateEquipmentPlacementContractAsync(CreateEquipmentPlacementContractRequestDto request);
    Task<ExecutionResult<List<GetEquipmentPlacementContractsResponseDto>>>GetEquipmentPlacementContractsAsync();
}