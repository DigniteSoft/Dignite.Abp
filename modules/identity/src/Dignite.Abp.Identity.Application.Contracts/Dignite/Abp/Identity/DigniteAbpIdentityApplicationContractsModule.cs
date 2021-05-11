using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Identity
{
    [DependsOn(
        typeof(AbpIdentityApplicationContractsModule)
        )]
    public class DigniteAbpIdentityApplicationContractsModule : AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }

    }
}
