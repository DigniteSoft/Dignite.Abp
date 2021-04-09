using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Dignite.AspNetCore.Mvc
{
    [DependsOn(
        typeof(AbpDddApplicationModule)
        )]
    public class DigniteAspNetCoreMvcContractsModule: AbpModule
    {
    }
}
