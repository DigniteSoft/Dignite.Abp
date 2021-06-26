using Dignite.Abp.Notifications.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.Notifications
{
    [DependsOn(
        typeof(AbpLocalizationModule)
        )]
    public class DigniteAbpNotificationsSharedModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DigniteAbpNotificationsResource>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<DigniteAbpNotificationsResource>("en")
                    .AddVirtualJson("/Dignite/Abp/Notifications/Localization");
            });
        }
    }
}
