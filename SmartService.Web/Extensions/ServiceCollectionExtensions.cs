namespace SmartService.Extensions;

using Core.Database;
using Microsoft.EntityFrameworkCore;

public static class ServiceCollectionExtensions
{
    public static void SetupServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<BaseDbContext>(d => d.UseSqlServer(connectionString));
    }
}