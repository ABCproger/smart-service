namespace SmartService.Core.Database.Entities;

public class EquipmentPlacementContract : BaseEntity
{
    public int ProductionFacilityId { get; set; }
    public ProductionFacility ProductionFacility { get; set; }
    
    public int ProcessEquipmentId { get; set; }
    public ProcessEquipment ProcessEquipment { get; set; }

    public int EquipmentUnits { get; set; }
}