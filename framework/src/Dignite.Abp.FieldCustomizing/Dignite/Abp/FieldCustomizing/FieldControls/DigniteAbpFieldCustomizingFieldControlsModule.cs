using Dignite.Abp.FieldCustomizing.Localization;
using Volo.Abp.Json;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.FieldCustomizing.FieldControls
{
    [DependsOn(
        typeof(AbpLocalizationModule),
        typeof(AbpJsonModule)
        )]
    public class DigniteAbpFieldCustomizingFieldControlsModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DigniteAbpFieldCustomizingFieldControlsModule>();
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
