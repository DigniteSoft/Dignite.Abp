using Dignite.Abp.BlobStoring.Localization;
using Volo.Abp.Authorization;
using Volo.Abp.BlobStoring;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.BlobStoring
{
    [DependsOn(
        typeof(AbpAuthorizationModule),
        typeof(AbpBlobStoringModule),
        typeof(AbpLocalizationModule)
        )]
    public class DigniteAbpBlobStoringModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DigniteAbpBlobStoringModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<DigniteAbpBlobStoringResource>("en")
                    .AddVirtualJson("Dignite/Abp/BlobStroring/Localization/Resources");
            });
        }
    }
}
