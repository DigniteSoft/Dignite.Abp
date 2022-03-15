using Microsoft.Extensions.DependencyInjection;
using Dignite.Abp.Identity.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Identity.Blazor;
using Dignite.Abp.AntDesignBlazorUI;

namespace Dignite.Abp.Identity.Blazor;

[DependsOn(
    typeof(DigniteAbpIdentityApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
        typeof(DigniteAbpAntDesignBlazorUIModule),
    typeof(AbpIdentityBlazorModule)
    )]
public class DigniteAbpIdentityBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<DigniteAbpIdentityBlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<IdentityBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new IdentityMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(DigniteAbpIdentityBlazorModule).Assembly);
        });
    }
}
