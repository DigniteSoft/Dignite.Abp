using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Dignite.Abp.Settings;

namespace Dignite.Abp.SettingManagement
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(DigniteSettingsModule)
    )]
    public class DigniteAbpSettingManagementApplicationContractsModule : AbpModule
    {
    }
}
