using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasiPosStock.Data;

/* This is used if database provider does't define
 * IEasiPosStockDbSchemaMigrator implementation.
 */
public class NullEasiPosStockDbSchemaMigrator : IEasiPosStockDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
