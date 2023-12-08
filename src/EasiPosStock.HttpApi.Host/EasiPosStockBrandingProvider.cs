using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace EasiPosStock;

[Dependency(ReplaceServices = true)]
public class EasiPosStockBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "EasiPosStock";
}
