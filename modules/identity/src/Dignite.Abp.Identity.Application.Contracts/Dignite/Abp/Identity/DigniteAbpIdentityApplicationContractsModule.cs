using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Identity
{
    [DependsOn(
        typeof(DigniteAbpIdentityDomainSharedModule),
        typeof(AbpIdentityApplicationContractsModule)
        )]
    public class DigniteAbpIdentityApplicationContractsModule : AbpModule
    {

    }
}
