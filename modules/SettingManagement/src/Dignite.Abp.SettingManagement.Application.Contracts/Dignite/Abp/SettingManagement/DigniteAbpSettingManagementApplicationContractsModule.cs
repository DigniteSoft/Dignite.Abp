using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Volo.Abp.Authorization;
using Dignite.Abp.FieldCustomizing;

namespace Dignite.Abp.SettingManagement
{
    [DependsOn(
        typeof(AbpSettingManagementDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationAbstractionsModule),
        typeof(DigniteAbpFieldCustomizingModule)
    )]
    public class DigniteAbpSettingManagementApplicationContractsModule : AbpModule
    {
    }
}
