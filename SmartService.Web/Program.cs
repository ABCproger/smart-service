using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SmartService.Authentication;
using SmartService.Core.Database;
using SmartService.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.SetupServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var serviceScopeFactory = app.Services.GetService<IServiceScopeFactory>();

using (var scope = serviceScopeFactory!.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.Run();