using Volo.Abp.Settings;

namespace EasiPosStock.Settings;

public class EasiPosStockSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(EasiPosStockSettings.MySetting1));
    }
}
