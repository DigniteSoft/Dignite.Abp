using Volo.Abp.Modularity;

namespace Dignite.Abp.Demo
{
    [DependsOn(
        typeof(DemoApplicationModule),
        typeof(DemoDomainTestModule)
        )]
    public class DemoApplicationTestModule : AbpModule
    {

    }
}