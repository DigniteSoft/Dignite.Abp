using Localization.Resources.AbpUi;
using Dignite.Abp.BlobStoringManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Abp.BlobStoringManagement
{
    [DependsOn(
        typeof(BlobStoringManagementApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class BlobStoringManagementHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(BlobStoringManagementHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<BlobStoringManagementResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });

            Configure<DigniteBlobEntityResolveOptions>(options =>
            {
                options.BlobEntityResolvers.Add(new QueryStringBlobEntityResolveContributor());
            });
        }
    }
}
