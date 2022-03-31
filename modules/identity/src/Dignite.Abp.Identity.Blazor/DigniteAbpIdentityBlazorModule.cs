using Microsoft.Extensions.DependencyInjection;
using Dignite.Abp.Identity.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Dignite.Abp.AntDesignBlazorUI;
using Volo.Abp.Threading;
using Volo.Abp.PermissionManagement.Blazor;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Identity;

namespace Dignite.Abp.Identity.Blazor;

[DependsOn(
    typeof(DigniteAbpIdentityApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(DigniteAbpAntDesignBlazorUIModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpPermissionManagementBlazorModule)
    )]
public class DigniteAbpIdentityBlazorModule : AbpModule
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();
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

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper
                .ApplyEntityConfigurationToUi(
                    IdentityModuleExtensionConsts.ModuleName,
                    IdentityModuleExtensionConsts.EntityNames.Role,
                    createFormTypes: new[] { typeof(IdentityRoleCreateDto) },
                    editFormTypes: new[] { typeof(IdentityRoleUpdateDto) }
                );

            ModuleExtensionConfigurationHelper
                .ApplyEntityConfigurationToUi(
                    IdentityModuleExtensionConsts.ModuleName,
                    IdentityModuleExtensionConsts.EntityNames.User,
                    createFormTypes: new[] { typeof(IdentityUserCreateDto) },
                    editFormTypes: new[] { typeof(IdentityUserUpdateDto) }
                );
        });
    }
}
