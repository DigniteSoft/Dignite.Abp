using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.Notifications.MongoDB
{
    [ConnectionStringName(NotificationsDbProperties.ConnectionStringName)]
    public class NotificationsMongoDbContext : AbpMongoDbContext, INotificationsMongoDbContext
    {
        public IMongoCollection<Notification> Notifications => Collection<Notification>();
        public IMongoCollection<UserNotification> UserNotifications => Collection<UserNotification>();
        public IMongoCollection<NotificationSubscription> NotificationSubscriptions => Collection<NotificationSubscription>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureNotifications();
        }
    }
}