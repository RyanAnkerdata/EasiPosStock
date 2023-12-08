using EasiPosStock.Localization;
using Volo.Abp.AspNetCore.Components;

namespace EasiPosStock.Blazor;

public abstract class EasiPosStockComponentBase : AbpComponentBase
{
    protected EasiPosStockComponentBase()
    {
        LocalizationResource = typeof(EasiPosStockResource);
    }
}
