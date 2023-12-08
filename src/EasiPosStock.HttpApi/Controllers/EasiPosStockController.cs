using EasiPosStock.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasiPosStock.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class EasiPosStockController : AbpControllerBase
{
    protected EasiPosStockController()
    {
        LocalizationResource = typeof(EasiPosStockResource);
    }
}
