using Volo.Abp.BlazoriseUI;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AntDesignBlazorUI
{
    [DependsOn(
        typeof(AbpBlazoriseUIModule)
        )]
    public class DigniteAbpAntDesignBlazorUIModule: AbpModule
    {
    }
}
