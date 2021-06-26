using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Notifications.EntityFrameworkCore
{
    [DependsOn(
        typeof(NotificationsDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class NotificationsEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<NotificationsDbContext>(options =>
            {
                options.AddRepository<Notification, EfCoreNotificationRepository>();
                options.AddRepository<UserNotification, EfCoreUserNotificationRepository>();
                options.AddRepository<NotificationSubscription, EfCoreNotificationSubscriptionRepository>();
            });
        }
    }
}