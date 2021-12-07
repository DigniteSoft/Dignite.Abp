using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.Modularity;

namespace Dignite.Abp.AspNetCore.Components.Web.PureTheme
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsWebThemingModule)
        )]
    public class DigniteAbpAspNetCoreComponentsWebPureThemeModule : AbpModule
    {
        
    }
}