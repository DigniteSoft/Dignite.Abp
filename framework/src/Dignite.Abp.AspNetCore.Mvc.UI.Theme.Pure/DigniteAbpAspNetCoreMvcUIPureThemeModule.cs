using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Bundling;
using Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Toolbars;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcUiThemeSharedModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule)
        )]
    public class DigniteAbpAspNetCoreMvcUIPureThemeModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(DigniteAbpAspNetCoreMvcUIPureThemeModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpThemingOptions>(options =>
            {
                options.Themes.Add<PureTheme>();

                if (options.DefaultThemeName == null)
                {
                    options.DefaultThemeName = PureTheme.Name;
                }
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DigniteAbpAspNetCoreMvcUIPureThemeModule>("Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure");
            });

            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new PureThemeMainTopToolbarContributor());
            });

            Configure<AbpBundlingOptions>(options =>
            {
                options
                    .StyleBundles
                    .Add(PureThemeBundles.Styles.Global, bundle =>
                    {
                        bundle
                            .AddContributors(typeof(PureThemeGlobalStyleContributor));
                    });

                options
                    .ScriptBundles
                    .Add(PureThemeBundles.Scripts.Global, bundle =>
                    {
                        bundle
                            .AddContributors(typeof(PureThemeGlobalScriptContributor));
                    });
            });

            context.Services.Configure<RazorViewEngineOptions>(options =>
            {
                var currentTenantLazy = context.Services.GetServiceLazy<ICurrentTenant>();
                options.ViewLocationExpanders.Add(new PureViewLocationExpander(currentTenantLazy));
            });
        }
    }
}
