using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Notifications
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(NotificationsDomainSharedModule)
    )]
    public class NotificationsDomainModule : AbpModule
    {

    }
}
