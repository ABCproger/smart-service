using Microsoft.EntityFrameworkCore;
using SmartService.Core.Database;
using SmartService.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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