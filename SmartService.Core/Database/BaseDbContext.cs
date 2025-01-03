namespace SmartService.Core.Database;

using Entities;
using Extensions;
using Microsoft.EntityFrameworkCore;
using Seeding;

public class BaseDbContext(DbContextOptions<BaseDbContext> options) : DbContext(options)
{
    public DbSet<EquipmentPlacementContract> EquipmentPlacementContracts { get; set; }
    public DbSet<ProcessEquipment> ProcessEquipments { get; set; }
    public DbSet<ProductionFacility> ProductionFacilities { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseDbContext).Assembly);
        modelBuilder.Seed();
        modelBuilder.OnDeleteRestrictRules();
        modelBuilder.AddNamingRules();
    }
}