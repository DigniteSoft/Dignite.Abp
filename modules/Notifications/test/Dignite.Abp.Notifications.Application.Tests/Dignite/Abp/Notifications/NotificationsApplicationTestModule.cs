using Volo.Abp.Modularity;

namespace Dignite.Abp.Notifications
{
    [DependsOn(
        typeof(NotificationsApplicationModule),
        typeof(NotificationsDomainTestModule)
        )]
    public class NotificationsApplicationTestModule : AbpModule
    {

    }
}
