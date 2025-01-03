namespace SmartService.Core.Database.Configurations;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProcessEquipmentConfiguration : IEntityTypeConfiguration<ProcessEquipment>
{
    public void Configure(EntityTypeBuilder<ProcessEquipment> builder)
    {
        builder.HasKey(pe => pe.Code);
        builder.Property(pe => pe.Code).IsRequired();
        builder.Property(pe => pe.Name).IsRequired();
        builder.Property(pe => pe.Area).IsRequired();
    }
}