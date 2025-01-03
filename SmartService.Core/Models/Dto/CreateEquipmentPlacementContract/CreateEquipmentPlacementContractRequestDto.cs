namespace SmartService.Core.Models.Dto.CreateEquipmentPlacementContract;

public class CreateEquipmentPlacementContractRequestDto
{
    public string ProductionFacilityCode { get; set; }
    public string ProccessQuipmentCode { get; set; }
    public int Quantity { get; set; }
}