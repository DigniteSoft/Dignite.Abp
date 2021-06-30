namespace Dignite.Abp.Settings
{
    public class TestSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingNavigation("test"),
                new Volo.Abp.Settings.SettingDefinition(TestSettingNames.TestSettingWithoutDefaultValue),
                new Volo.Abp.Settings.SettingDefinition(TestSettingNames.TestSettingWithDefaultValue, "default-value"),
                new Volo.Abp.Settings.SettingDefinition(TestSettingNames.TestSettingEncrypted, isEncrypted: true)
            );
        }
    }
}
