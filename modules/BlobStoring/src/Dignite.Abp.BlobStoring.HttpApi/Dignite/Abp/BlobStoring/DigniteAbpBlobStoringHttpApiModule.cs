using Dignite.Abp.BlobStoring.Localization;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Dignite.Abp.BlobStoring
{
    [DependsOn(
        typeof(DigniteAbpBlobStoringContractsModule),
        typeof(AbpAspNetCoreMvcModule)
    )]
    public class DigniteAbpBlobStoringHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(DigniteAbpBlobStoringHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<BlobStoringResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
