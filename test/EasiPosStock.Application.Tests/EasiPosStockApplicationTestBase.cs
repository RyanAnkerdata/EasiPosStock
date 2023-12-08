using Volo.Abp.Modularity;

namespace EasiPosStock;

public abstract class EasiPosStockApplicationTestBase<TStartupModule> : EasiPosStockTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
