namespace SmartService.Extensions;

using Core.Database;
using Core.Services.EquipmentPlacement;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

public static class ServiceCollectionExtensions
{
    public static void SetupServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<BaseDbContext>(d => d.UseSqlServer(connectionString,
            o =>
            {
                o.MigrationsAssembly("SmartService.Web");
            }));
        
        services.AddTransient<IEquipmentPlacementService, EquipmentPlacementService>();


        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<BaseDbContext>();
    }
}