using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Dignite.Abp.Settings;
using Dignite.FieldCustomizing;

namespace Dignite.Abp.SettingManagement
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(DigniteSettingsModule),
        typeof(DigniteFieldCustomizingModule)
    )]
    public class DigniteAbpSettingManagementApplicationContractsModule : AbpModule
    {
    }
}
