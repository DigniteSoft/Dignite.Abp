using Dignite.Abp.FieldCustomizing.TextboxForm;

namespace Dignite.Abp.Settings
{
    public class TestSettingDefinitionProvider : DigniteSettingDefinitionProvider
    {
        public override void Define(IDigniteSettingDefinitionContext context)
        {
            context.Add(
                new SettingNavigation(TestSettingNames.TestSettingNavigationName),
                new Volo.Abp.Settings.SettingDefinition(TestSettingNames.TestSettingWithoutDefaultValue),                
                new Volo.Abp.Settings.SettingDefinition(TestSettingNames.TestSettingWithDefaultValue, "default-value")
                    .SetForm(form =>
                        form.UseTextbox(tb =>
                        {
                            tb.Required = true;
                            tb.Placeholder = "placeholder-text";
                        }
                    )),
                new Volo.Abp.Settings.SettingDefinition(TestSettingNames.TestSettingEncrypted, isEncrypted: true)
            ) ;
        }
    }
}
