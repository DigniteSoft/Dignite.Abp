using System.Collections.Generic;
using System.Collections.Immutable;
using Volo.Abp.Settings;
using Dignite.Abp.Settings.SettingItemControls;
using Volo.Abp.DependencyInjection;

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

    public class TestPackageSettingDefinitionProvider : TestSettingDefinitionProvider, IDigniteSettingDefinitionProvider, ITransientDependency
    {
        public void Define(IDigniteSettingDefinitionContext context)
        {
            context.SetNavigation(TestSettingNames.TestSettingNavigationName2);

            var settings = new Dictionary<string, SettingDefinition>();
            Define(new SettingDefinitionContext(settings));

            settings.GetValueOrDefault(TestSettingNames.TestSettingPackager)
                    .UseTextbox(tb =>                        
                        {
                            tb.Required = true;
                            tb.Placeholder = "placeholder-text";
                        }
                    );                
        }
    }
}
