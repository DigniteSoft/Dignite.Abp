using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Blazor.Server;

namespace Dignite.Abp.Identity.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(DigniteAbpIdentityBlazorModule),
    typeof(AbpPermissionManagementBlazorServerModule)
    )]
public class DigniteAbpIdentityBlazorServerModule : AbpModule
{

}
