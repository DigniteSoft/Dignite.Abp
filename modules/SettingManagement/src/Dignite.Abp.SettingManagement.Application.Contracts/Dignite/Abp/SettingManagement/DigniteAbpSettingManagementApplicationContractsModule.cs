using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Dignite.Abp.Settings;
using Dignite.Abp.FieldCustomizing;
using Volo.Abp.SettingManagement;
using Volo.Abp.Authorization;

namespace Dignite.Abp.SettingManagement
{
    [DependsOn(
        typeof(AbpSettingManagementDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationAbstractionsModule),
        typeof(DigniteAbpSettingsModule)
    )]
    public class DigniteAbpSettingManagementApplicationContractsModule : AbpModule
    {
    }
}
