using Dignite.FieldCustomizing;
using Volo.Abp.Modularity;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings
{
    [DependsOn(
        typeof(AbpSettingsModule),
        typeof(DigniteFieldCustomizingModule)
        )]
    public class DigniteSettingsModule:AbpModule
    {
    }
}
