using Volo.Abp.Modularity;

namespace EasiPosStock;

/* Inherit from this class for your domain layer tests. */
public abstract class EasiPosStockDomainTestBase<TStartupModule> : EasiPosStockTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
