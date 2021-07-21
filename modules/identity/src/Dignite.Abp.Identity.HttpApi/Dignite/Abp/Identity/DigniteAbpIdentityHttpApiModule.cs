using Dignite.Abp.Identity.Localization;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Identity
{
    [DependsOn(
          typeof(AbpIdentityHttpApiModule),
          typeof(DigniteAbpIdentityApplicationContractsModule),
          typeof(AbpAspNetCoreMvcModule))]
    public class DigniteAbpIdentityHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(DigniteAbpIdentityHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<IdentityOrganizationUnitResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
