
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace Dignite.Abp.SettingManagement
{
    [DependsOn(
        typeof(AbpSettingManagementDomainModule),
        typeof(DigniteAbpSettingManagementApplicationContractsModule)
    )]
    public class DigniteSettingManagementApplicationModule : AbpModule
    {
    }
}
