using Dignite.Abp.Notifications.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Dignite.Abp.Notifications
{
    [DependsOn(
        typeof(DigniteAbpNotificationsModule)
    )]
    public class NotificationsDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NotificationsDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<NotificationResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Dignite/Abp/Notifications/Localization");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Notifications", typeof(NotificationResource));
            });
        }
    }
}
