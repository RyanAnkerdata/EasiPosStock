using EasiPosStock.Localization;
using Volo.Abp.Application.Services;

namespace EasiPosStock;

/* Inherit your application services from this class.
 */
public abstract class EasiPosStockAppService : ApplicationService
{
    protected EasiPosStockAppService()
    {
        LocalizationResource = typeof(EasiPosStockResource);
    }
}
