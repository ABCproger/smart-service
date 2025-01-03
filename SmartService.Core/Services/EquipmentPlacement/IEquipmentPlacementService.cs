namespace SmartService.Core.Services.EquipmentPlacement;

using Models.Dto.CreateEquipmentPlacementContract;
using Models.Dto.GetEquipmentPlacementContracts;

public interface IEquipmentPlacementService
{
    Task CreateEquipmentPlacementContractAsync(CreateEquipmentPlacementContractRequestDto request);
    Task<List<GetEquipmentPlacementContractsResponseDto>>GetEquipmentPlacementContractsAsync();
}