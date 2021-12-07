using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Dignite.Abp.SettingManagement.Blazor.WebAssembly
{
    [DependsOn(
        typeof(SettingManagementBlazorModule),
        typeof(DigniteAbpSettingManagementHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
        )]
    public class SettingManagementBlazorWebAssemblyModule : AbpModule
    {
        
    }
}