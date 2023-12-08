using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EasiPosStock.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class EasiPosStockDbContextFactory : IDesignTimeDbContextFactory<EasiPosStockDbContext>
{
    public EasiPosStockDbContext CreateDbContext(string[] args)
    {
        EasiPosStockEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<EasiPosStockDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new EasiPosStockDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EasiPosStock.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
