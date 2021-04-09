using Dignite.Abp.BlobStoring.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.BlobStoring
{
    [DependsOn(
        typeof(AbpLocalizationModule)
        )]
    public class DigniteAbpBlobStoringSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DigniteAbpBlobStoringSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<BlobStoringResource>("en")
                    .AddVirtualJson("Dignite/Abp/BlobStroring/Localization/Resources");
            });
        }
    }
}
