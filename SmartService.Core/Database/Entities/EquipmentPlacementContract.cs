namespace SmartService.Core.Database.Entities;

public class EquipmentPlacementContract
{
    public int Id { get; set; }
    
    public string ProductionFacilityCode { get; set; }
    public ProductionFacility ProductionFacility { get; set; }

    public string ProcessEquipmentCode { get; set; }
    public ProcessEquipment ProcessEquipment { get; set; }

    public int EquipmentUnits { get; set; }
}
