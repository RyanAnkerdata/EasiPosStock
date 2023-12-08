using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EasiPosStock.Data;
using Volo.Abp.DependencyInjection;

namespace EasiPosStock.EntityFrameworkCore;

public class EntityFrameworkCoreEasiPosStockDbSchemaMigrator
    : IEasiPosStockDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreEasiPosStockDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the EasiPosStockDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<EasiPosStockDbContext>()
            .Database
            .MigrateAsync();
    }
}
