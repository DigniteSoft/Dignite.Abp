using Dignite.Abp.AntDesignBlazorUI.WebAssembly;
using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Identity.Blazor.WebAssembly;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Identity.Blazor.WebAssembly;

[DependsOn(
    typeof(DigniteAbpIdentityBlazorModule),
    typeof(DigniteAbpIdentityHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule),
    typeof(AbpIdentityBlazorWebAssemblyModule),
        typeof(DigniteAbpAntDesignBlazorUIWebAssemblyModule)
    )]
public class IdentityBlazorWebAssemblyModule : AbpModule
{

}
