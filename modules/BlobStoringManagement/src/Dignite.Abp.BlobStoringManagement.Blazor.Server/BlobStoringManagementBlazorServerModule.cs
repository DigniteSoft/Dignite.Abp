using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Dignite.Abp.BlobStoringManagement.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(BlobStoringManagementBlazorModule)
        )]
    public class BlobStoringManagementBlazorServerModule : AbpModule
    {
        
    }
}