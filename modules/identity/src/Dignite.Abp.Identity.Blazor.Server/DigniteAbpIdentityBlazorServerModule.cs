using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
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
    public override void ConfigureServices(ServiceConfigurationContext context)
    {

        Configure<AbpBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Add(BlazorDigniteAbpIdentityBundles.Styles.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(BlazorDigniteAbpIdentityBundles.Styles.Global)
                        .AddContributors(typeof(BlazorDigniteAbpIdentityStyleContributor));
                });

            options
                .ScriptBundles
                .Add(BlazorDigniteAbpIdentityBundles.Scripts.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(BlazorDigniteAbpIdentityBundles.Scripts.Global)
                        .AddContributors(typeof(BlazorDigniteAbpIdentityScriptContributor));
                });
        });
    }
}
