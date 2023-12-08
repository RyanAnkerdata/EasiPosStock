using EasiPosStock.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace EasiPosStock.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EasiPosStockEntityFrameworkCoreModule),
    typeof(EasiPosStockApplicationContractsModule)
)]
public class EasiPosStockDbMigratorModule : AbpModule
{
}
