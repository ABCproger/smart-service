namespace SmartService.Core.Database.Configurations;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductionFacilityConfiguration: IEntityTypeConfiguration<ProductionFacility>
{
    public void Configure(EntityTypeBuilder<ProductionFacility> builder)
    {
        builder.HasKey(pe => pe.Code);
        builder.Property(pe => pe.Code).IsRequired();
        builder.Property(pe => pe.Name).IsRequired();
        builder.Property(pe => pe.StandardArea).IsRequired();
    }
}