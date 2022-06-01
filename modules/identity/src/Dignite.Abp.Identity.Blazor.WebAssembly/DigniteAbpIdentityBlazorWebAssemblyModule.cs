using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Blazor.WebAssembly;

namespace Dignite.Abp.Identity.Blazor.WebAssembly;

[DependsOn(
    typeof(DigniteAbpIdentityBlazorModule),
    typeof(DigniteAbpIdentityHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule),
    typeof(AbpPermissionManagementBlazorWebAssemblyModule)
    )]
public class DigniteAbpIdentityBlazorWebAssemblyModule : AbpModule
{

}
