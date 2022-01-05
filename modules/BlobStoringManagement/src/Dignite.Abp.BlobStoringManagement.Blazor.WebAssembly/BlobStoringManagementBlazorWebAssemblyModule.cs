using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Dignite.Abp.BlobStoringManagement.Blazor.WebAssembly
{
    [DependsOn(
        typeof(BlobStoringManagementBlazorModule),
        typeof(BlobStoringManagementHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
        )]
    public class BlobStoringManagementBlazorWebAssemblyModule : AbpModule
    {
        
    }
}