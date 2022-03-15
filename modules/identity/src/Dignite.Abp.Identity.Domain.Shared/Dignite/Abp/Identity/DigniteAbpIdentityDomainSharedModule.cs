using Dignite.Abp.Identity.Localization;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.Identity
{

    [DependsOn(
        typeof(AbpIdentityDomainSharedModule)
    )]
    public class DigniteAbpIdentityDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DigniteAbpIdentityDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<DigniteAbpIdentityResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Dignite/Abp/Identity/Localization");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Dignite.Abp.Identity", typeof(DigniteAbpIdentityResource));
            });
        }
    }
}