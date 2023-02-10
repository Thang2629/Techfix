using Volo.Abp.Settings;

namespace Managerment.Settings;

public class ManagermentSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ManagermentSettings.MySetting1));
    }
}
