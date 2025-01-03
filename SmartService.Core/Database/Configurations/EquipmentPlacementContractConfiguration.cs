namespace SmartService.Core.Database.Configurations;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EquipmentPlacementContractConfiguration: IEntityTypeConfiguration<EquipmentPlacementContract>
{
    public void Configure(EntityTypeBuilder<EquipmentPlacementContract> builder)
    {
        builder.HasKey(epc => epc.Id);
        
        builder.HasOne(epc => epc.ProductionFacility)
            .WithMany(pf => pf.EquipmentPlacementContracts)
            .HasForeignKey(epc => epc.ProductionFacilityCode)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(epc => epc.ProcessEquipment)
            .WithMany(pe => pe.EquipmentPlacementContracts)
            .HasForeignKey(epc => epc.ProcessEquipmentCode)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}