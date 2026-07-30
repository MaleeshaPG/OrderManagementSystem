using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Data;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Tests;

public class DatabaseInitializationTests
{
    [Fact]
    public async Task InitializeAsync_CreatesIdentityTablesAndSeedsDefaultRoles()
    {
        var tempDbPath = Path.Combine(Path.GetTempPath(), $"oms-test-{Guid.NewGuid():N}.db");

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddDbContext<OMSDbContext>(options =>
            options.UseSqlite($"Data Source={tempDbPath}"));

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<OMSDbContext>()
        .AddDefaultTokenProviders();

        using var serviceProvider = services.BuildServiceProvider();

        await DbInitializer.InitializeAsync(serviceProvider);

        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<OMSDbContext>();
        var migrations = await context.Database.GetAppliedMigrationsAsync();

        Assert.NotEmpty(migrations);

        await using var connection = context.Database.GetDbConnection();
        await connection.OpenAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='AspNetRoles'";
        var tableName = await command.ExecuteScalarAsync();

        Assert.Equal("AspNetRoles", tableName);

        await context.Database.CloseConnectionAsync();
        await connection.DisposeAsync();
        await context.DisposeAsync();
        serviceProvider.Dispose();

        GC.Collect();
        GC.WaitForPendingFinalizers();

        if (File.Exists(tempDbPath))
        {
            try
            {
                File.Delete(tempDbPath);
            }
            catch (IOException)
            {
                // SQLite can keep the temporary file handle alive briefly after the context is disposed.
            }
        }
    }
}
