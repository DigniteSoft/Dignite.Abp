
using Dignite.Abp.FieldCustomizing.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.FieldCustomizing
{
    [DependsOn(
        typeof(AbpLocalizationModule),
        typeof(AbpThreadingModule)
        )]
    public class DigniteAbpFieldCustomizingModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DigniteAbpFieldCustomizingModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<DigniteAbpFieldCustomizingResource>("en")
                    .AddVirtualJson("/Dignite/Abp/FieldCustomizing/Localization/Resources");
            });
        }
    }
}
