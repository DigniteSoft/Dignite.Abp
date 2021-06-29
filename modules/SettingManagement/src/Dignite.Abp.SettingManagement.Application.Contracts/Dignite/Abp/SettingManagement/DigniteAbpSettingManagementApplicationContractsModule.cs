using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Dignite.Abp.Settings;
using Dignite.Abp.FieldCustomizing;
using Volo.Abp.SettingManagement;

namespace Dignite.Abp.SettingManagement
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(DigniteAbpSettingsModule),
        typeof(AbpSettingManagementDomainSharedModule),
        typeof(DigniteAbpFieldCustomizingModule)
    )]
    public class DigniteAbpSettingManagementApplicationContractsModule : AbpModule
    {
    }
}
