using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Dignite.Abp.SettingManagement.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(SettingManagementBlazorModule)
        )]
    public class SettingManagementBlazorServerModule : AbpModule
    {
        
    }
}