using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Identity.Blazor.Server;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Identity.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(AbpIdentityBlazorServerModule),
    typeof(DigniteAbpIdentityBlazorModule)
    )]
public class DigniteAbpIdentityBlazorServerModule : AbpModule
{

}
