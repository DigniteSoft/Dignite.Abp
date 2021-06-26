using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.Notifications.MongoDB
{
    [DependsOn(
        typeof(NotificationsDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class NotificationsMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<NotificationsMongoDbContext>(options =>
            {
                options.AddRepository<Notification, MongoNotificationRepository>();
                options.AddRepository<UserNotification, MongoUserNotificationRepository>();
                options.AddRepository<NotificationSubscription, MongoNotificationSubscriptionRepository>();
            });
        }
    }
}
