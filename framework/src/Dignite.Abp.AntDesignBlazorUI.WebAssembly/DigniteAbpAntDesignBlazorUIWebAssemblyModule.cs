
using Volo.Abp.Modularity;

namespace Dignite.Abp.AntDesignBlazorUI.WebAssembly
{
    [DependsOn(
        typeof(DigniteAbpAntDesignBlazorUIModule)
        )]
    public class DigniteAbpAntDesignBlazorUIWebAssemblyModule: AbpModule
    {
    }
}
