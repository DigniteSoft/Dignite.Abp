using Dignite.Abp.Demo.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Demo
{
    [DependsOn(
        typeof(DemoEntityFrameworkCoreTestModule)
        )]
    public class DemoDomainTestModule : AbpModule
    {

    }
}