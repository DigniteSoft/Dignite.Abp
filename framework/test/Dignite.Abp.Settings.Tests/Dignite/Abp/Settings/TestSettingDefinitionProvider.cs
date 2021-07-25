using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Volo.Abp.Settings;
using Dignite.Abp.FieldCustomizing.TextboxForm;

namespace Dignite.Abp.Settings
{
    public class TestSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingDefinition(TestSettingNames.TestSettingPackager,"abc")
            );
        }
    }

    public class TestPackageSettingDefinitionProvider : TestSettingDefinitionProvider, IDigniteSettingDefinitionProvider
    {
        public void Define(IDigniteSettingDefinitionContext context)
        {
            var settings = new Dictionary<string, SettingDefinition>();
            Define(new SettingDefinitionContext(settings));

            settings.GetValueOrDefault(TestSettingNames.TestSettingPackager)
                    .SetForm(form =>
                        form.UseTextbox(tb =>
                        {
                            tb.Required = true;
                            tb.Placeholder = "placeholder-text";
                        }
                    ));                

            context.Add(
                new SettingNavigation(TestSettingNames.TestSettingNavigationName2),
                settings.Values.ToImmutableList().ToArray()
            );
        }
    }
}
