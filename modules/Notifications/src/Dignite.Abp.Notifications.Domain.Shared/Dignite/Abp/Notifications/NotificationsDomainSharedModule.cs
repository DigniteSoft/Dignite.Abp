using Volo.Abp.Modularity;

namespace Dignite.Abp.Notifications
{
    [DependsOn(
        typeof(DigniteAbpNotificationsSharedModule)
    )]
    public class NotificationsDomainSharedModule : AbpModule
    {
    }
}
