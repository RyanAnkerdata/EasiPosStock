using Volo.Abp.Modularity;

namespace EasiPosStock;

[DependsOn(
    typeof(EasiPosStockApplicationModule),
    typeof(EasiPosStockDomainTestModule)
)]
public class EasiPosStockApplicationTestModule : AbpModule
{

}
