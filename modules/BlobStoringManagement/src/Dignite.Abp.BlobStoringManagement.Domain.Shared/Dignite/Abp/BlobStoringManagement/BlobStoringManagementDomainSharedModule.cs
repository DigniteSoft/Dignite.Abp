using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Dignite.Abp.BlobStoringManagement.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Dignite.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoringManagement
{
    [DependsOn(
        typeof(AbpValidationModule),
        typeof(DigniteAbpBlobStoringModule)
    )]
    public class BlobStoringManagementDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<BlobStoringManagementDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<BlobStoringManagementResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Dignite/Abp/BlobStoringManagement/Localization/Resources");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Dignite.Abp.BlobStoringManagement", typeof(BlobStoringManagementResource));
            });
        }
    }
}
