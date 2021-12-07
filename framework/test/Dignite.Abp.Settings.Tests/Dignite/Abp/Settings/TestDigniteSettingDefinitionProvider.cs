
using Dignite.Abp.Settings.SettingItemControls;

namespace Dignite.Abp.Settings
{
    public class TestDigniteSettingDefinitionProvider : DigniteSettingDefinitionProvider
    {
        public override void Define(IDigniteSettingDefinitionContext context)
        {
            context.Add(
                new SettingNavigation(TestSettingNames.TestSettingNavigationName),
                new Volo.Abp.Settings.SettingDefinition(TestSettingNames.TestSettingWithoutDefaultValue),                
                new Volo.Abp.Settings.SettingDefinition(TestSettingNames.TestSettingWithDefaultValue, "default-value")
                    .UseTextbox(tb =>
                        {
                            tb.Required = true;
                            tb.Placeholder = "placeholder-text";
                            tb.CharLimit = 64;
                        }
                    ),
                new Volo.Abp.Settings.SettingDefinition(TestSettingNames.TestSettingEncrypted, isEncrypted: true)
            ) ;
        }
    }
}
