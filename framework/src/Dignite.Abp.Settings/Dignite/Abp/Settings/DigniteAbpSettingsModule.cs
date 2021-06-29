
using Dignite.Abp.FieldCustomizing;
using Volo.Abp.Modularity;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings
{
    [DependsOn(
        typeof(AbpSettingsModule),
        typeof(DigniteAbpFieldCustomizingModule)
        )]
    public class DigniteAbpSettingsModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}
