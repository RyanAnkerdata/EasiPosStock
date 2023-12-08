using System.Threading.Tasks;

namespace EasiPosStock.Data;

public interface IEasiPosStockDbSchemaMigrator
{
    Task MigrateAsync();
}
