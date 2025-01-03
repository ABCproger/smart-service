namespace SmartService.Core.Database;

using Extensions;
using Microsoft.EntityFrameworkCore;
using Seeding;

public class BaseDbContext(DbContextOptions<BaseDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Seed();
        modelBuilder.OnDeleteRestrictRules();
        modelBuilder.AddNamingRules();
    }
}