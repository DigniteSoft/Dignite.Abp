using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Notifications
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(NotificationsDomainSharedModule),
        typeof(DigniteAbpNotificationsModule)
    )]
    public class NotificationsDomainModule : AbpModule
    {

    }
}
