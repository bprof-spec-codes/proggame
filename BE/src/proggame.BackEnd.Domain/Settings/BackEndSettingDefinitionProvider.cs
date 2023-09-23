using Volo.Abp.Settings;

namespace proggame.BackEnd.Settings;

public class BackEndSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(BackEndSettings.MySetting1));
    }
}
