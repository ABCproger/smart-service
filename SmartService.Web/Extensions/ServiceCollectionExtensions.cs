namespace SmartService.Extensions;

using Authentication;
using BackgroundServices;
using Core.Database;
using Core.Services.EquipmentPlacement;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

public static class ServiceCollectionExtensions
{
    public static void SetupServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(f => f.Filters.Add<ApiKeyAuthFilter>());

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = "The API key to access the API",
                Type = SecuritySchemeType.ApiKey,
                Name = "x-api-key",
                In = ParameterLocation.Header,
                Scheme = "ApiKeyScheme"
            });

            var scheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                In = ParameterLocation.Header
            };

            var requirement = new OpenApiSecurityRequirement
            {
                {
                    scheme, new List<string>()
                }
            };
            c.AddSecurityRequirement(requirement);
        });
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<BaseDbContext>(d => d.UseSqlServer(connectionString,
            o =>
            {
                o.MigrationsAssembly("SmartService.Web");
            }));
        
        services.AddTransient<IEquipmentPlacementService, EquipmentPlacementService>();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<BaseDbContext>();
        
        services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
        services.AddHostedService<BackgroundTaskProcessor>();
    }
}