using Dignite.Abp.AntDesignBlazorUI.WebAssembly;
using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Blazor.WebAssembly;

namespace Dignite.Abp.Identity.Blazor.WebAssembly;

[DependsOn(
    typeof(DigniteAbpIdentityBlazorModule),
    typeof(DigniteAbpIdentityHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule),
        typeof(DigniteAbpAntDesignBlazorUIWebAssemblyModule),
    typeof(AbpPermissionManagementBlazorWebAssemblyModule)
    )]
public class IdentityBlazorWebAssemblyModule : AbpModule
{

}
