using Dignite.Abp.Settings.Localization;
using Dignite.FieldCustomizing;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Settings;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.Settings
{
    [DependsOn(
        typeof(AbpSettingsModule),
        typeof(DigniteFieldCustomizingModule)
        )]
    public class DigniteSettingsModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DigniteSettingsModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<DigniteAbpSettingsResource>("en")
                    .AddVirtualJson("/Dignite/Abp/Settings/Localization/Resources");
            });
        }
    }
}
