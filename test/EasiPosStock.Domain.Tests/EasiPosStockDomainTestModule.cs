using Volo.Abp.Modularity;

namespace EasiPosStock;

[DependsOn(
    typeof(EasiPosStockDomainModule),
    typeof(EasiPosStockTestBaseModule)
)]
public class EasiPosStockDomainTestModule : AbpModule
{

}
