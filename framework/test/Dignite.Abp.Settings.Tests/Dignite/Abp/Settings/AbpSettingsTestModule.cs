using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Settings
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(DigniteAbpSettingsModule),
        typeof(AbpTestBaseModule)
        )]
    public class AbpSettingsTestModule : AbpModule
    {
    }
}
