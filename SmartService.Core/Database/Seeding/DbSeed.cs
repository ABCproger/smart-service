namespace SmartService.Core.Database.Seeding;

using Entities;
using Microsoft.EntityFrameworkCore;

public static class DbSeed
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductionFacility>().HasData(
            new ProductionFacility
            {
                Code = "FAC001",
                Name = "Main Production Facility",
                StandardArea = 5000.0
            },
            new ProductionFacility
            {
                Code = "FAC002",
                Name = "Secondary Facility",
                StandardArea = 3000.0
            }
        );
        
        modelBuilder.Entity<ProcessEquipment>().HasData(
            new ProcessEquipment
            {
                Code = "EQP001",
                Name = "High-Speed Conveyor",
                Area = 200.0
            },
            new ProcessEquipment
            {
                Code = "EQP002",
                Name = "Industrial Robot Arm",
                Area = 150.0
            }
        );

        modelBuilder.Entity<EquipmentPlacementContract>().HasData(
            new EquipmentPlacementContract
            {
                Id = 1,
                ProductionFacilityCode = "FAC001",
                ProcessEquipmentCode = "EQP001",
                EquipmentUnits = 10
            },
            new EquipmentPlacementContract
            {
                Id = 2,
                ProductionFacilityCode = "FAC002",
                ProcessEquipmentCode = "EQP002",
                EquipmentUnits = 5
            }
        );
    }
}
